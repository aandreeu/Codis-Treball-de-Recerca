using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialoguesThroughSequences : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject CharchterImage;
    [SerializeField] private DialogueLine[] Lines;

    [SerializeField] private bool IsPlayerInRange;
    public bool DidDilogueStart;
    public bool DidDialogueEnd;
    [SerializeField] private float WaitingTime;
    public int lineIndex;

    public PlayerController playerController;
    public PlayerHudController playerHudController;
    public CinemachineVirtualCamera PlayerCamera;
    public float OriginalCameraPos_y = 0.4f;
    public float DialogueCameraPos_y = -1.97f;

    [SerializeField] float[] TimeCheckpointInSequence;
    [SerializeField] PlayableDirector playableDirector;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHudController = FindObjectOfType<PlayerHudController>();
        PlayerCamera = FindObjectOfType<CinemachineVirtualCamera>();
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

        SceneLoaderForPlayer[] loaders = FindObjectsOfType<SceneLoaderForPlayer>();
        foreach (SceneLoaderForPlayer _loader in loaders)
        {
            _loader.CanPlayerTp = false;
        }

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
            CharchterImage.SetActive(false);
            //Descongelar player
            playerController.canMove = true;
            playerController.UnfreezePlayer();
            playerHudController.AbleOpenMenu();

            DialogueText.text=string.Empty;

            SceneLoaderForPlayer[] loaders = FindObjectsOfType<SceneLoaderForPlayer>();
            foreach (SceneLoaderForPlayer _loader in loaders)
            {
                _loader.CanPlayerTp = true;
            }

            this.enabled = false;
            this.hideFlags = HideFlags.HideInInspector;
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
    public void SumarLinea()
    {
        lineIndex++;
    }
    public void CridarStartDialogue()
    {
        StartDialogue();
    }
    public void ComprovarSiCalFerLoop(int LineIndexToBe)
    {
        if (LineIndexToBe < lineIndex)
        {
            playableDirector.time = TimeCheckpointInSequence[0];
        }
    }
}
