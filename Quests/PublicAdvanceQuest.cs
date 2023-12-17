using UnityEngine;

public class PublicAdvanceQuest : MonoBehaviour
{
    [SerializeField] string QuestTitleToCheck;
    [SerializeField] int QuestProgressionToCheck;
    [SerializeField] QuestPlayerHandler questPlayerHandler;

    public void AdvanceQuest()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();

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
