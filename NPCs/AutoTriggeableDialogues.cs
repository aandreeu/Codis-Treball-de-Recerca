using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using Cinemachine;

public class AutoTriggeableDialogues : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject CharchterImage;
    [SerializeField] private DialogueLine[] Lines;

    [SerializeField] private bool IsPlayerInRange;
    [SerializeField] private bool Trucada;
    public bool DidDilogueStart;
    public bool DidDialogueEnd;
    [SerializeField] private float WaitingTime;
    public int lineIndex;

    public PlayerController playerController;
    [SerializeField] Animator playeranimator;
    public PlayerHudController playerHudController;
    public CinemachineVirtualCamera PlayerCamera;
    public float OriginalCameraPos_y = 0.4f;
    public float DialogueCameraPos_y = -1.97f;

    public string QuestTitleToCheck;
    public int QuestProgressionToCheck;
    public QuestPlayerHandler questHandler;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHudController = FindObjectOfType<PlayerHudController>();
        PlayerCamera = FindObjectOfType<CinemachineVirtualCamera>();
        questHandler = FindObjectOfType<QuestPlayerHandler>();

        IsPlayerInRange = false;
    }

    void Update()
    {
        if (DidDilogueStart && Input.GetKeyDown("e") && playerHudController.Menu.activeInHierarchy == false)
        {
            if (DialogueText.text == Lines[lineIndex].DialogueLineTxt)
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = Lines[lineIndex].DialogueLineTxt;
            }
        }
    }
    private void StartDialogue() //iniciar dialeg i activar huds
    {
        lineIndex = 0;

        DidDilogueStart = true;
        DidDialogueEnd = false;
        DialoguePanel.SetActive(true);
        CharchterImage.SetActive(true);
        NameText.text = Lines[lineIndex].Speaker.Name;
        CharchterImage.GetComponent<Image>().sprite = Lines[lineIndex].Speaker.Face;
        //Jugador: Congelarlo
        playerController.FreezePlayer();
        playerHudController.CantOpenMenu();
        //Moure Camera
        PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = DialogueCameraPos_y;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < Lines.Length) //Passar linea
        {
            NameText.text = Lines[lineIndex].Speaker.Name;
            CharchterImage.GetComponent<Image>().sprite = Lines[lineIndex].Speaker.Face;
            StartCoroutine(ShowLine());
        }
        else //Acabar
        {
            //Animar penjar trucada
            playeranimator.SetTrigger("AmagarMobil");
            //Pujar camera a estat original
            PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = OriginalCameraPos_y;
            //Reiniciar variables
            DidDilogueStart = false;
            DidDialogueEnd = true;
            //Desactivar HUD
            DialoguePanel.SetActive(false);
            CharchterImage.SetActive(false);
            //Descongelar player
            playerController.canMove = true;
            playerController.UnfreezePlayer();
            playerHudController.AbleOpenMenu();

            SceneLoaderForPlayer[] loaders = FindObjectsOfType<SceneLoaderForPlayer>();
            foreach (SceneLoaderForPlayer _loader in loaders)
            {
                _loader.CanPlayerTp = true;
            }

            this.enabled = false;
            this.hideFlags= HideFlags.HideInInspector;
        }
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;
        foreach (char ch in Lines[lineIndex].DialogueLineTxt)
        {
            DialogueText.text += ch;
            yield return new WaitForSeconds(WaitingTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        questHandler = FindObjectOfType<QuestPlayerHandler>();
        if (collision.gameObject.CompareTag("Player") && QuestTitleToCheck == questHandler.quest.QuestTitle && QuestProgressionToCheck == questHandler.quest.QuestPorgression)
        {
            IsPlayerInRange = true;
            playeranimator = collision.GetComponent<Animator>();
            SceneLoaderForPlayer[] loaders = FindObjectsOfType<SceneLoaderForPlayer>();
            foreach (SceneLoaderForPlayer _loader in loaders)
            {
                _loader.CanPlayerTp = false;
            }

            if (Trucada)
            {
                playeranimator.Play("TreureMobil_Player");
                playerController.FreezePlayer();
                playerHudController.CantOpenMenu();
                Invoke("StartDialogue", 1.3f);
            }
            else if (!Trucada)
            {
                playeranimator.SetFloat("VelocitatCaminar", 0f);
                playeranimator.Play("Idle_Player");
                StartDialogue();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            
        }
    }
    public void SumarLinea()
    {
        lineIndex++;
    }
    public void CridarStartDialogue()
    {
        StartDialogue();
    }
}
