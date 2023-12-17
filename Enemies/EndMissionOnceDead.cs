using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMissionOnceDead : MonoBehaviour
{
    public QuestPlayerHandler questPlayerHandler;
    public PlayerInventory playerInventory;
    [SerializeField] string QuestTitleToCheck;
    [SerializeField] int QuestProgressionToCheck;

    public void EndOrAdvanceMission()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();

        if (questPlayerHandler.quest.QuestPorgression + 1 == questPlayerHandler.quest.QuestPhases.Length) //Acabar la missio si s arriba al final
        {
            Debug.Log("Acaba la missio");
            questPlayerHandler.quest.QuestGoal.IsReached = true;
            var playerInventory = FindObjectOfType<PlayerInventory>();
            playerInventory.CurrentMoney += questPlayerHandler.quest.MoneyReward;
            this.enabled = false;
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitleToCheck && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCheck)
        {
            Debug.Log("Avanca la missio");
            questPlayerHandler.quest.QuestPorgression++;
            this.enabled = false;
        }
    }
}
