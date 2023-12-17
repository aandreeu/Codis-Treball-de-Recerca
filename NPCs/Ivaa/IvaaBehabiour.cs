using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvaaBehabiour : MonoBehaviour
{
    [SerializeField] Dialogues[] Dialogues;
    [SerializeField] AdvanceQuestThroughDialogue[] AdvanceQuestThroughDialogue;
    [SerializeField] DonarCaixa donarCaixa;
    [Header("Player")]
    [SerializeField] QuestPlayerHandler playerHandler;
    [Header("Quests Data")]
    [SerializeField] string[] QuestTitleToCompare;
    [SerializeField] int[] QuestProgressionToCompare;

    private void OnEnable()
    {
        #region Apagar-ho tot
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        Dialogues[0].enabled = false;
        Dialogues[1].enabled = false;
        Dialogues[2].enabled = false;
        Dialogues[3].enabled = false;
        Dialogues[4].enabled = false;
        Dialogues[5].enabled = false;
        Dialogues[6].enabled = false;
        Dialogues[7].enabled = false;
        donarCaixa.enabled = false;
        AdvanceQuestThroughDialogue[0].enabled = false;
        AdvanceQuestThroughDialogue[1].enabled = false;
        AdvanceQuestThroughDialogue[2].enabled = false;
        AdvanceQuestThroughDialogue[3].enabled = false;
        AdvanceQuestThroughDialogue[4].enabled = false;
        AdvanceQuestThroughDialogue[5].enabled = false;
        #endregion

        if (playerHandler.quest.QuestTitle == QuestTitleToCompare[0] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[0])
        {
            Dialogues[0].enabled = true;
            AdvanceQuestThroughDialogue[0].enabled = true;
            donarCaixa.enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[1] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[1])
        {
            Dialogues[0].enabled = false;
            Dialogues[1].enabled = true;
            AdvanceQuestThroughDialogue[0].enabled = false;
            AdvanceQuestThroughDialogue[1].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[2] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[2])
        {
            Dialogues[1].enabled = false;
            Dialogues[2].enabled = true;
            AdvanceQuestThroughDialogue[1].enabled = false;
            AdvanceQuestThroughDialogue[2].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[3] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[3])
        {
            Dialogues[2].enabled = false;
            Dialogues[3].enabled = true;
            AdvanceQuestThroughDialogue[2].enabled = false;
            AdvanceQuestThroughDialogue[3].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[4] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[4])
        {
            Dialogues[3].enabled = false;
            Dialogues[4].enabled = true;
            AdvanceQuestThroughDialogue[3].enabled = false;
            AdvanceQuestThroughDialogue[4].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[5] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[5])
        {
            Dialogues[4].enabled = false;
            Dialogues[5].enabled = true;
            AdvanceQuestThroughDialogue[4].enabled = false;
            AdvanceQuestThroughDialogue[5].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[6] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[6])
        {
            Dialogues[5].enabled = false;
            Dialogues[7].enabled = true;
            AdvanceQuestThroughDialogue[5].enabled = false;
            AdvanceQuestThroughDialogue[6].enabled = true;
        }
        else
        {
            foreach(Dialogues d in Dialogues)
            {
                d.enabled = false;
            }
            foreach(AdvanceQuestThroughDialogue a in AdvanceQuestThroughDialogue)
            {
                a.enabled = false;
            }
            Dialogues[6].enabled = true; //Dialegs de compra normal
        }
    }

}
