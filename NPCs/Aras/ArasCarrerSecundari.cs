using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArasCarrerSecundari : MonoBehaviour
{

    [SerializeField] string[] QuestTitles;
    [SerializeField] int[] QuestProgressions;

    [SerializeField] Dialogues[] Dialogues;
    [SerializeField] Collider2D[] colliders;
    [SerializeField] GameObject ProtaDialogue;
    [SerializeField] AdvanceQuestThroughDialogue[] AdvanceQuestThroughDialogue;
    [SerializeField] PlaySequenceThroughDialogue playSequence;
    [SerializeField] ArasBossController ArasBossController;
    [SerializeField] bool haAnimat=false;
    public RuntimeAnimatorController[] animatorControllers;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] GameObject Prota_GO;
    [SerializeField] ArasController Aras_playerController;
    [SerializeField] ArasMeleeCombat Aras_meleeCombat;
    [SerializeField] PlayerController Prota_playerController;
    [SerializeField] MeleeCombat Prota_meleeCombat;
    [SerializeField] GameObject Aras_GO;
    [SerializeField] CinemachineVirtualCamera gameCamera;
    [Header("Rival Prota")]
    [SerializeField] GameObject triggerAnimacio;
    [SerializeField] RivalProta_Behaviour rivalProta_Behaviour;


    void Update()
    {
        if (questPlayerHandler.quest.QuestTitle == QuestTitles[0] && questPlayerHandler.quest.QuestPorgression == QuestProgressions[0])
        {
            Debug.Log("0");
            Dialogues[0].enabled = true;
            colliders[0].enabled = true;
            AdvanceQuestThroughDialogue[0].enabled = true;
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitles[1] && questPlayerHandler.quest.QuestPorgression == QuestProgressions[1])
        {
            Debug.Log("1");
            colliders[0].enabled = false;
            colliders[1].enabled = true;
            colliders[2].enabled = true;
            Dialogues[0].enabled = false;
            AdvanceQuestThroughDialogue[0].enabled = false;
            Aras_meleeCombat.enabled = true;
            Aras_playerController.enabled = true;
            Prota_meleeCombat.enabled = false;
            if (Prota_playerController.speed != 0)
            {
                Prota_playerController.FreezePlayer();
            }
            gameCamera.Follow = Aras_GO.transform;
            gameCamera.LookAt = Aras_GO.transform;
            gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
            if (!haAnimat)
            {
                gameObject.GetComponent<Animator>().Play("IdleSenseJaqueta_Aras");
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[0];
                gameObject.tag = "Player";

                haAnimat = true;
            }
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitles[2] && questPlayerHandler.quest.QuestPorgression == QuestProgressions[2])
        {
            Debug.Log("2");
            Dialogues[0].enabled = false;
            colliders[0].enabled = true;
            AdvanceQuestThroughDialogue[0].enabled = false;
            ProtaDialogue.SetActive(true);
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitles[3] && questPlayerHandler.quest.QuestPorgression == QuestProgressions[3])
        {
            Debug.Log("3");
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = false;
            Destroy(Aras_meleeCombat);
            Destroy(Aras_playerController);
            gameObject.layer =0;
            Prota_meleeCombat.enabled = true;
            Prota_playerController.UnfreezePlayer();
            Prota_playerController.speed = 8f;
            gameCamera.Follow = Prota_GO.transform;
            gameCamera.LookAt = Prota_GO.transform;
            gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[1];
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            ProtaDialogue.SetActive(false);
            triggerAnimacio.SetActive(true);
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitles[4] && questPlayerHandler.quest.QuestPorgression == QuestProgressions[4])
        {
            Debug.Log("4");
            Dialogues[1].enabled = true;
            colliders[0].enabled = true;
            colliders[1].enabled = true;
            colliders[2].enabled = true;
            AdvanceQuestThroughDialogue[1].enabled = true;
            playSequence.enabled= true;
            gameObject.tag = "Enemy";
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitles[5] && questPlayerHandler.quest.QuestPorgression == QuestProgressions[5])
        {
            Debug.Log("5");
            Dialogues[1].enabled = false;
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitles[6] && questPlayerHandler.quest.QuestPorgression == QuestProgressions[6])
        {
            Debug.Log("6");
            Dialogues[2].enabled = true;
            colliders[0].enabled = true;
            AdvanceQuestThroughDialogue[2].enabled = true;
        }
    }

    private void OnEnable()
    {
        colliders[0].enabled = true;
        colliders[1].enabled = false;
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        Prota_meleeCombat = GameObject.Find("Player").GetComponent<MeleeCombat>();
        Prota_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameCamera = FindObjectOfType<CinemachineVirtualCamera>();
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Prota_GO = GameObject.Find("Player");
        if (questPlayerHandler.quest.QuestTitle != QuestTitles[0])
        {
            Aras_GO.SetActive(false);
        }
        else
        {
            Aras_GO.SetActive(true);
            Aras_playerController.enabled= false;
            Aras_meleeCombat.enabled= false;
            Dialogues[1].enabled = false;
            AdvanceQuestThroughDialogue[1].enabled = false;
            ProtaDialogue.SetActive(false);
            playSequence.enabled=false;
            ArasBossController.enabled= false;
        }
    }
}
