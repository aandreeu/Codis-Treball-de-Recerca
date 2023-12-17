using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfItemInInventory : MonoBehaviour
{
    public Item itemToCheck;
    public PlayerInventory playerInventory;
    [SerializeField] string QuestTitleToCheck;
    [SerializeField] int QuestProgressionToCheck;

    public void CheckItemInInventory()
    {
        playerInventory= FindObjectOfType<PlayerInventory>();
        playerInventory.inventory.CheckForItem(itemToCheck);
    }
    public void CheckItemAndAdvanceMission()
    {
        playerInventory= FindObjectOfType<PlayerInventory>();
        QuestPlayerHandler questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (playerInventory.inventory.CheckForItem(itemToCheck)){ 

            if (questPlayerHandler.quest.QuestPorgression + 1 == questPlayerHandler.quest.QuestPhases.Length && QuestTitleToCheck == questPlayerHandler.quest.QuestTitle && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCheck) //Acabar la missio si s arriba al final
            {
                Debug.Log("Acaba la missio");
                questPlayerHandler.quest.QuestGoal.IsReached = true;
                var playerInventory = FindObjectOfType<PlayerInventory>();
                playerInventory.CurrentMoney += questPlayerHandler.quest.MoneyReward;
            }
            else if (QuestTitleToCheck == questPlayerHandler.quest.QuestTitle && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCheck)
            {
                Debug.Log("Avanca la missio: " + QuestTitleToCheck);
                Debug.Log("Cridat per: " + gameObject.name);
                questPlayerHandler.quest.QuestPorgression++;
            }
        }
    }
}
