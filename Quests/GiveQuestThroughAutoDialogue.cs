using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveQuestThroughAutoDialogue : MonoBehaviour
{
    [SerializeField] int DialogueLineToGive;

    public AutoTriggeableDialogues AutoDialoguesScript;
    [SerializeField] QuestDefiner quest;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    private void Start()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
    }

    private void Update()
    {
        if (DialogueLineToGive == AutoDialoguesScript.lineIndex)
        {
            quest.IsQuestActive = true;
            questPlayerHandler.quest = quest;
            this.enabled = false;
        }
    }
}
