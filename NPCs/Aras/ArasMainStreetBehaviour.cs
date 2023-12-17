using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArasMainStreetBehaviour : MonoBehaviour
{
    [SerializeField] Dialogues[] dialogues;
    [SerializeField] GiveQuestThroughDialogue giveQuest;
    [SerializeField] AdvanceQuestThroughDialogue advanceQuest;
    [SerializeField] QuestPlayerHandler questHandler;

    [SerializeField] string[] QuestTitlesToCompare;
    [SerializeField] int[] QuestProgressionToCompare;

    void Start()
    {
        questHandler = FindObjectOfType<QuestPlayerHandler>();

        dialogues[0].enabled = true;
        dialogues[1].enabled = false;
        giveQuest.enabled = true;
        advanceQuest.enabled = false;
    }

    void Update()
    {
        if (questHandler.quest.QuestTitle == QuestTitlesToCompare[0] && questHandler.quest.QuestPorgression == QuestProgressionToCompare[0])
        {
            dialogues[0].enabled = true;
            dialogues[1].enabled = false;
            giveQuest.enabled = true;
            advanceQuest.enabled = false;
        }
        else if (questHandler.quest.QuestTitle == QuestTitlesToCompare[1] && questHandler.quest.QuestPorgression == QuestProgressionToCompare[1])
        {
            dialogues[0].enabled = false;
            dialogues[1].enabled = true;
            giveQuest.enabled = false;
            advanceQuest.enabled = true;
        }
        else
        {
            foreach(Dialogues d in dialogues)
            {
                d.enabled = false;
            }
            giveQuest.enabled = false;
            advanceQuest.enabled = false;
        }
    }
}
