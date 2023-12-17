using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceQuestThroughAutoDialogue : MonoBehaviour
{
    [SerializeField] int DialogueLineToAdvance;
    [SerializeField] string QuestTitleToCheck;
    [SerializeField] int QuestProgressionToCheck;
    public AutoTriggeableDialogues AutoDialoguesScript;
    [SerializeField] QuestPlayerHandler questPlayerHandler;

    private void Start()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
    }

    private void Update()
    {
        if (questPlayerHandler.quest.QuestPorgression + 1 == questPlayerHandler.quest.QuestPhases.Length && DialogueLineToAdvance == AutoDialoguesScript.lineIndex && QuestTitleToCheck == questPlayerHandler.quest.QuestTitle && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCheck) //Acabar la missio si s arriba al final
        {
            Debug.Log("Acaba la missio");
            questPlayerHandler.quest.QuestGoal.IsReached = true;
            var playerInventory = FindObjectOfType<PlayerInventory>();
            playerInventory.CurrentMoney += questPlayerHandler.quest.MoneyReward;
            this.enabled = false;
            this.hideFlags = HideFlags.HideInInspector;
        }
        else if (DialogueLineToAdvance == AutoDialoguesScript.lineIndex && QuestTitleToCheck == questPlayerHandler.quest.QuestTitle && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCheck)
        {
            Debug.Log("Avanca la missio: " + QuestTitleToCheck);
            Debug.Log("Cridat per: " + gameObject.name);
            questPlayerHandler.quest.QuestPorgression++;
            this.enabled = false;
            this.hideFlags = HideFlags.HideInInspector;
        }
    }
}
