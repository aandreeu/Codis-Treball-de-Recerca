using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using Cinemachine;

public class Dialogues : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject PanelE;
    [SerializeField] private Animator Animator_PressE;
    [SerializeField] private GameObject CharchterImage;
    [SerializeField] private DialogueLine[] Lines;

    [SerializeField] private bool IsPlayerInRange;
    public bool DestroyOnEnd = false;
    public bool DidDilogueStart;
    public bool DidDialogueEnd;
    public bool CanKeepReading;
    [SerializeField] private float WaitingTime;
    public int lineIndex;

    public PlayerController playerController;
    public PlayerHudController playerHudController;
    public CinemachineVirtualCamera PlayerCamera;
    public float OriginalCameraPos_y = 0.4f;
    public float DialogueCameraPos_y = -1.97f;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHudController = FindObjectOfType<PlayerHudController>();
        PlayerCamera = FindObjectOfType<CinemachineVirtualCamera>();

        IsPlayerInRange = false;
        CanKeepReading= true;
        PanelE.SetActive(false);
    }
    void Update()
    {
        if (IsPlayerInRange && Input.GetKeyDown("e") && CanKeepReading && playerHudController.Menu.activeInHierarchy==false)
        {
            if (!DidDilogueStart) //Si esta al rang, comenca el dialeg
            {
                StartDialogue();
            }
            else if (DialogueText.text == Lines[lineIndex].DialogueLineTxt) //Si ha acabat d escriure la linia, mostra la seguent
            {
                NextDialogueLine();
            }
            else //Si apreta a la e mentre encara s esta escrivint, l acaba de completar
            {
                StopAllCoroutines();
                DialogueText.text = Lines[lineIndex].DialogueLineTxt;
            }
        }
    }
    private void StartDialogue() //iniciar dialeg i activar huds
    {
        lineIndex = 0;

        Animator_PressE.SetBool("HasToLoop", true);
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
            //Pujar camera a estat original
            PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = OriginalCameraPos_y;
            //Reiniciar variables
            DidDilogueStart = false;
            DidDialogueEnd = true;
            //Desactivar HUD
            DialoguePanel.SetActive(false);
            CharchterImage.SetActive(false) ;
            //Descongelar player
            playerController.canMove = true;
            playerController.UnfreezePlayer();
            playerHudController.AbleOpenMenu();
            if(DestroyOnEnd)
            {
                this.enabled= false;
                Animator_PressE.SetBool("HasToLoop", false);
            }
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
        if (collision.gameObject.CompareTag("Player") && this.enabled==true)
        {
            IsPlayerInRange = true;
            PanelE.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            Animator_PressE.SetBool("HasToLoop", false);
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

    public void CanKeepReading_TrueFalse(bool trueFalse)
    {
        CanKeepReading= trueFalse;
    }
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(4, 6)] public string DialogueLineTxt;
    public NPCProfiles Speaker;
}