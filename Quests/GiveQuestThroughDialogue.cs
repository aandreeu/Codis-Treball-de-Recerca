using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveQuestThroughDialogue : MonoBehaviour
{
    [SerializeField] int DialogueLineToGive;

    public Dialogues DialoguesScript;
    [SerializeField] QuestDefiner quest;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    private void Start()
    {
        questPlayerHandler= FindObjectOfType<QuestPlayerHandler>();
    }

    private void Update()
    {
        if (DialogueLineToGive == DialoguesScript.lineIndex)
        {
            quest.IsQuestActive= true;
            questPlayerHandler.quest = quest;
            this.enabled = false;
        }
    }
}
