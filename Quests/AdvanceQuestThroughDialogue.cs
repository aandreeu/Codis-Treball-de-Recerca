using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceQuestThroughDialogue : MonoBehaviour
{
    [SerializeField] int DialogueLineToAdvance;
    [SerializeField] string QuestTitleToCheck;
    [SerializeField] int QuestProgressionToCheck;
    public Dialogues DialoguesScript;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] bool HasBeenDone;

    private void Start()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
    }

    private void Update()
    {
        if (!HasBeenDone && questPlayerHandler.quest.QuestPorgression + 1 == questPlayerHandler.quest.QuestPhases.Length && DialogueLineToAdvance == DialoguesScript.lineIndex && QuestTitleToCheck == questPlayerHandler.quest.QuestTitle && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCheck) //Acabar la missio si s arriba al final
        {
            Debug.Log("Acaba la missio");
            questPlayerHandler.quest.QuestGoal.IsReached = true;
            var playerInventory = FindObjectOfType<PlayerInventory>();
            playerInventory.ManipulateMoney(questPlayerHandler.quest.MoneyReward);
            HasBeenDone = true;
            this.enabled = false;
            this.hideFlags= HideFlags.HideInInspector;
        }
        else if (!HasBeenDone && DialogueLineToAdvance == DialoguesScript.lineIndex && QuestTitleToCheck == questPlayerHandler.quest.QuestTitle && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCheck)
        {
            Debug.Log("Avanca la missio: "+ QuestTitleToCheck);
            Debug.Log("Cridat per: "+ gameObject.name);
            questPlayerHandler.quest.QuestPorgression++;
            HasBeenDone = true;
            this.enabled = false;
            this.hideFlags = HideFlags.HideInInspector;
        }
    }
}
