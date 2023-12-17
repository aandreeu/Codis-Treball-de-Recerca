using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BarnelBehaviour : MonoBehaviour
{
    [SerializeField] Dialogues[] Dialogues;
    [SerializeField] PlaySequenceThroughDialogue[] playSequenceThroughDialogues;
    [SerializeField] AdvanceQuestThroughDialogue[] AdvanceQuestThroughDialogue;
    [SerializeField] GiveQuestThroughDialogue giveQuestThroughDialogue;
    [Header("Player")]
    [SerializeField] QuestPlayerHandler playerHandler;

    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] TimelineAsset[] timelines;

    [SerializeField] GameObject Barnel_Gnl_GO;
    [SerializeField] Transform ArasBarnel_Pos;
    [SerializeField] Transform ArasBarnelOriginal_Pos;

    public void Start()
    {
        playerHandler=FindObjectOfType<QuestPlayerHandler>();
        Dialogues[0].enabled = true;
        Dialogues[1].enabled = false;
        Dialogues[2].enabled = false;
        Dialogues[3].enabled = false;
        Dialogues[4].enabled = false;
        Dialogues[5].enabled = false;
        AdvanceQuestThroughDialogue[0].enabled = true;
        AdvanceQuestThroughDialogue[1].enabled = false;
        AdvanceQuestThroughDialogue[2].enabled = false;
        AdvanceQuestThroughDialogue[3].enabled = false;
        AdvanceQuestThroughDialogue[4].enabled = false;
        AdvanceQuestThroughDialogue[5].enabled = false;
        giveQuestThroughDialogue.enabled = false;
    }

    public void Update()
    {
        if (playerHandler.quest.QuestTitle=="Segon dia de feina." && playerHandler.quest.QuestPorgression==4)
        {
            Destroy(Dialogues[0]);
            Dialogues[1].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]);
            AdvanceQuestThroughDialogue[1].enabled = true;
            giveQuestThroughDialogue.enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == "Tercer dia de feina." && playerHandler.quest.QuestPorgression == 1)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Dialogues[2].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            AdvanceQuestThroughDialogue[2].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Tercer dia de feina." && playerHandler.quest.QuestPorgression == 3)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Dialogues[3].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            AdvanceQuestThroughDialogue[3].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Zeros." && playerHandler.quest.QuestPorgression == 2)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Zeros." && playerHandler.quest.QuestPorgression == 5)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Dialogues[4].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            AdvanceQuestThroughDialogue[4].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Quart dia de feina." && playerHandler.quest.QuestPorgression == 3)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(Dialogues[4]);
            Dialogues[5].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(AdvanceQuestThroughDialogue[4]);
            AdvanceQuestThroughDialogue[5].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Quart dia de feina." && playerHandler.quest.QuestPorgression == 6)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(Dialogues[4]);
            Destroy(Dialogues[5]);
            Dialogues[6].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(AdvanceQuestThroughDialogue[4]);
            Destroy(AdvanceQuestThroughDialogue[5]);
            AdvanceQuestThroughDialogue[6].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Compres [Cinquè dia de feina]." && playerHandler.quest.QuestPorgression == 6)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(Dialogues[4]);
            Destroy(Dialogues[5]);
            Destroy(Dialogues[6]);
            Dialogues[7].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(AdvanceQuestThroughDialogue[4]);
            Destroy(AdvanceQuestThroughDialogue[5]);
            Destroy(AdvanceQuestThroughDialogue[6]);
            AdvanceQuestThroughDialogue[7].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Compres [Cinquè dia de feina]." && playerHandler.quest.QuestPorgression == 9)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(Dialogues[4]);
            Destroy(Dialogues[5]);
            Destroy(Dialogues[6]);
            Destroy(Dialogues[7]);
            Dialogues[8].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(AdvanceQuestThroughDialogue[4]);
            Destroy(AdvanceQuestThroughDialogue[5]);
            Destroy(AdvanceQuestThroughDialogue[6]);
            Destroy(AdvanceQuestThroughDialogue[7]);
            AdvanceQuestThroughDialogue[8].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Indici de llum." && playerHandler.quest.QuestPorgression == 15)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(Dialogues[4]);
            Destroy(Dialogues[5]);
            Destroy(Dialogues[6]);
            Destroy(Dialogues[7]);
            Destroy(Dialogues[8]);
            Dialogues[9].enabled = true;
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(AdvanceQuestThroughDialogue[4]);
            Destroy(AdvanceQuestThroughDialogue[5]);
            Destroy(AdvanceQuestThroughDialogue[6]);
            Destroy(AdvanceQuestThroughDialogue[7]);
            Destroy(AdvanceQuestThroughDialogue[8]);
            AdvanceQuestThroughDialogue[9].enabled = true;
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Indici de llum." && playerHandler.quest.QuestPorgression == 17)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(Dialogues[4]);
            Destroy(Dialogues[5]);
            Destroy(Dialogues[6]);
            Destroy(Dialogues[7]);
            Destroy(Dialogues[8]);
            Destroy(Dialogues[9]);
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(AdvanceQuestThroughDialogue[4]);
            Destroy(AdvanceQuestThroughDialogue[5]);
            Destroy(AdvanceQuestThroughDialogue[6]);
            Destroy(AdvanceQuestThroughDialogue[7]);
            Destroy(AdvanceQuestThroughDialogue[8]);
            Destroy(AdvanceQuestThroughDialogue[9]);
            Destroy(giveQuestThroughDialogue);
        }
        else if (playerHandler.quest.QuestTitle == "Indici de llum." && playerHandler.quest.QuestPorgression == 18)
        {
            Destroy(Dialogues[0]);//Al joc final es pot treure
            Destroy(Dialogues[1]);
            Destroy(Dialogues[2]);
            Destroy(Dialogues[3]);
            Destroy(Dialogues[4]);
            Destroy(Dialogues[5]);
            Destroy(Dialogues[6]);
            Destroy(Dialogues[7]);
            Destroy(Dialogues[8]);
            Destroy(Dialogues[9]);
            Destroy(playSequenceThroughDialogues[0]);
            Destroy(playSequenceThroughDialogues[1]);
            Destroy(AdvanceQuestThroughDialogue[0]); //Al joc final es pot treure
            Destroy(AdvanceQuestThroughDialogue[1]);
            Destroy(AdvanceQuestThroughDialogue[2]);
            Destroy(AdvanceQuestThroughDialogue[3]);
            Destroy(AdvanceQuestThroughDialogue[4]);
            Destroy(AdvanceQuestThroughDialogue[5]);
            Destroy(AdvanceQuestThroughDialogue[6]);
            Destroy(AdvanceQuestThroughDialogue[7]);
            Destroy(AdvanceQuestThroughDialogue[8]);
            Destroy(AdvanceQuestThroughDialogue[9]);
            Destroy(giveQuestThroughDialogue);
        }

    }

    private void OnEnable()
    {
        playerHandler=FindObjectOfType<QuestPlayerHandler>();
        if (playerHandler.quest.QuestTitle == "Quart dia de feina.")
        {
            Barnel_Gnl_GO.transform.position = ArasBarnelOriginal_Pos.position;
            playableDirector.playableAsset = timelines[1];
        }
        else if (playerHandler.quest.QuestTitle == "Compres [Cinquè dia de feina].")
        {
            Barnel_Gnl_GO.transform.position = ArasBarnel_Pos.position;
            playableDirector.playableAsset = timelines[0];
        }
        else if (playerHandler.quest.QuestTitle == "Indici de llum.")
        {
            Barnel_Gnl_GO.transform.position = ArasBarnel_Pos.position;
            Debug.Log("Barnel");
        }
        else
        {
            Barnel_Gnl_GO.transform.position = ArasBarnelOriginal_Pos.position;
        }
    }
}
