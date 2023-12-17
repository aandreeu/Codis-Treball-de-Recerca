using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed_Behaviour : MonoBehaviour
{
    [SerializeField] string[] QuestTitles;
    [SerializeField] int[] QuestProgressions;
    [SerializeField] QuestPlayerHandler playerHandler;
    [SerializeField] SleepInBed sleepInBed;

    [SerializeField] AdvanceQuestProgression[] advanceQuest;
    [SerializeField] GiveQuestThroughInteraction[] giveQuestThroughInteraction;
    [SerializeField] AutoTriggeableDialogues AutoTriggerDialogue;
    [SerializeField] Dialogues[] dialogues;
    [SerializeField] AdvanceQuestThroughDialogue[] advanceQuestThroughDialogues;

    private void Start()
    {
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        AutoTriggerDialogue.enabled = false;
        giveQuestThroughInteraction[0].enabled = false;
        giveQuestThroughInteraction[1].enabled = false;
        giveQuestThroughInteraction[2].enabled = false;
        advanceQuest[0].enabled = false;
        advanceQuest[1].enabled = false;
        dialogues[0].enabled = false;

    }
    private void Update()
    {
        if (playerHandler.quest.QuestTitle == QuestTitles[0] && playerHandler.quest.QuestPorgression == QuestProgressions[0])
        {
            AutoTriggerDialogue.enabled = false;
            advanceQuest[0].enabled = false;
            giveQuestThroughInteraction[0].enabled = true;
            giveQuestThroughInteraction[1].enabled = false;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[1] && playerHandler.quest.QuestPorgression == QuestProgressions[1])
        {
            AutoTriggerDialogue.enabled = false;
            advanceQuest[0].enabled = true;
            giveQuestThroughInteraction[0].enabled = false;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[2] && playerHandler.quest.QuestPorgression == QuestProgressions[2])
        {
            AutoTriggerDialogue.enabled = true;
            advanceQuest[0].enabled = false;
            giveQuestThroughInteraction[0].enabled = false;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[3] && playerHandler.quest.QuestPorgression == QuestProgressions[3])
        {
            AutoTriggerDialogue.enabled = false;
            advanceQuest[0].enabled = false;
            giveQuestThroughInteraction[0].enabled = false;
            dialogues[0].enabled=true;
            advanceQuestThroughDialogues[0].enabled = true;
            sleepInBed.CanSleep = false;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[4] && playerHandler.quest.QuestPorgression == QuestProgressions[4])
        {
            dialogues[0].enabled=false;
            advanceQuestThroughDialogues[0].enabled=false;
            giveQuestThroughInteraction[1].enabled = true;
            sleepInBed.CanSleep= true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[5] && playerHandler.quest.QuestPorgression == QuestProgressions[5])
        {
            dialogues[0].enabled=false;
            advanceQuestThroughDialogues[0].enabled=false;
            giveQuestThroughInteraction[1].enabled = false;
            giveQuestThroughInteraction[2].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[6] && playerHandler.quest.QuestPorgression == QuestProgressions[6])
        {
            sleepInBed.CanSleep = true;
            advanceQuest[2].enabled = true;
        }
        else
        {
            AutoTriggerDialogue.enabled = false;
            giveQuestThroughInteraction[0].enabled = false;
            giveQuestThroughInteraction[1].enabled = false;
            giveQuestThroughInteraction[2].enabled = false;
            advanceQuest[0].enabled = false;
            dialogues[0].enabled = false;
        }
    }
}
