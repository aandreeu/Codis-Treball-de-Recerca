using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinijocAtzar1 : MonoBehaviour
{
    public int PlayerPickedNumber;
    [SerializeField] int MaxPickeableNumber;
    public int MachinePick;
    public PlayerInventory playerInventory;
    public PlayerController playerController;
    public PlayerHudController playerHudController;
    public TriggerMinigame triggerMinigame;
    public float moneyMultiplier;
    public float MoneyBet;

    [SerializeField] bool HasPlayerPicked;
    [SerializeField] bool CanGenerateNumber;

    public TMP_Text MaxNumberDisplayer;
    public TMP_Text CurrentMoney;
    public TMP_Text PotentialMoney;
    public TMP_Text MultiplierDisplay;

    #region GAME OBJECTS MINIJOC
    public GameObject GamePanelGeneralGO;
    public GameObject SumMoneyGO;
    public GameObject RestMoneyGO;
    public GameObject PotentialMoneyGO;
    public GameObject CurrentMoneyGO;
    public GameObject MultiplierMoneyGO;
    public GameObject PlayButtonGO;
    public Button Boto1;
    public Button Boto2;
    public Button Boto3;
    public Button Boto4;
    public Button Boto5;
    public Button Boto6;
    public Button Boto7;
    public Button Boto8;
    public Button Boto9;
    #endregion

    private void Start()
    {
        HasPlayerPicked = false;
        CanGenerateNumber= false;
        playerInventory=FindObjectOfType<PlayerInventory>();
        playerController=FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        MaxNumberDisplayer.text = MaxPickeableNumber.ToString();
        MultiplierDisplay.text = "x"+ moneyMultiplier.ToString("F2");
        CurrentMoney.text= MoneyBet.ToString("F0");
        PotentialMoney.text=(moneyMultiplier*MoneyBet).ToString("F0");
    }
    public void StartGame() //Resetar els valors
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHudController= FindObjectOfType<PlayerHudController>();
        playerHudController.CantOpenMenu();
        playerController.FreezePlayer();
        MachinePick = 0;
        PlayerPickedNumber= 0;
        MoneyBet = 0f;
        MaxPickeableNumber= 0;
        CanGenerateNumber = false;
        HasPlayerPicked = false;
        Boto1.interactable = false;
        GamePanelGeneralGO.SetActive(true);
        SumMoneyGO.SetActive(true);
        RestMoneyGO.SetActive(true);
        PotentialMoneyGO.SetActive(true) ;
        CurrentMoneyGO.SetActive(true);
        MultiplierMoneyGO.SetActive(true) ;
        PlayButtonGO.SetActive(true);
        Boto2.interactable= true;
        Boto3.interactable = true;
        Boto4.interactable = true;
        Boto5.interactable = true;
        Boto6.interactable = true;
        Boto7.interactable = true;
        Boto8.interactable = true;
        Boto9.interactable = true;
    }
    public void PlayerPick(int PickNumber)//Al apretar boto
    {
        if (!HasPlayerPicked && !CanGenerateNumber)//Per definir amb quants es jugara
        {
            MaxPickeableNumber = PickNumber;
        }
        else if (!HasPlayerPicked && CanGenerateNumber)//Per definir el pick del player
        {
            PlayerPickedNumber= PickNumber;
        }
        moneyMultiplier = 1 + ((float)MaxPickeableNumber * 0.1f);
    }
    public void Play()//Per generar el num de la maquina i desactivar els botons inecesaris
    {
        CanGenerateNumber = true;
        playerInventory.CurrentMoney -= MoneyBet;
        MachinePick=Random.Range(1, MaxPickeableNumber+1);
        if(MaxPickeableNumber==2)
        {
            Boto3.interactable = false;
            Boto4.interactable = false;
            Boto5.interactable = false;
            Boto6.interactable = false;
            Boto7.interactable = false;
            Boto8.interactable = false;
            Boto9.interactable = false;
        }
        else if(MaxPickeableNumber==3)
        {
            Boto4.interactable = false;
            Boto5.interactable = false;
            Boto6.interactable = false;
            Boto7.interactable = false;
            Boto8.interactable = false;
            Boto9.interactable = false;
        }
        else if(MaxPickeableNumber==4)
        {
            Boto5.interactable = false;
            Boto6.interactable = false;
            Boto7.interactable = false;
            Boto8.interactable = false;
            Boto9.interactable = false;
        }
        else if(MaxPickeableNumber==5)
        {
            Boto6.interactable = false;
            Boto7.interactable = false;
            Boto8.interactable = false;
            Boto9.interactable = false;
        }
        else if(MaxPickeableNumber==6)
        {
            Boto7.interactable = false;
            Boto8.interactable = false;
            Boto9.interactable = false;
        }
        else if(MaxPickeableNumber==7)
        {
            Boto8.interactable = false;
            Boto9.interactable = false;
        }
        else if(MaxPickeableNumber==8)
        {
            Boto9.interactable = false;
        }
    }
    public void ChangeBettedMoney(float diference)//Per definir els diners apostats
    {
        if (MoneyBet <= playerInventory.CurrentMoney)
        {
            MoneyBet += diference;
            if (MoneyBet < 0)
            {
                MoneyBet = 0;
            }
            else if (MoneyBet > playerInventory.CurrentMoney){
                MoneyBet=playerInventory.CurrentMoney;
            }
        }
    }
    public void CheckNumber() //Comprobar numero i acabar
    {
        playerController = FindObjectOfType<PlayerController>();
        HasPlayerPicked = true;
        playerController.UnfreezePlayer();
        playerHudController.AbleOpenMenu();
        triggerMinigame.CanPlay = true;

        if (MachinePick == PlayerPickedNumber)
        {
            playerInventory.ManipulateMoney(MoneyBet * moneyMultiplier);
            Debug.Log("Guanyat");
        }
        gameObject.SetActive(false);
    }

}