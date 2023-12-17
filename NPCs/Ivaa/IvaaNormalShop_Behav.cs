using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvaaNormalShop_Behav : MonoBehaviour
{
    [SerializeField] string QuestTitleToCompare;
    [SerializeField] int QuestProgressionToCompare;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] Dialogues dialogues;

    private void Update()
    {
        if (questPlayerHandler.quest.QuestTitle == QuestTitleToCompare && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCompare+1)
        {
            dialogues.enabled = true;
        }
        else
        {
            dialogues.enabled = false;
        }
    }

    private void OnEnable()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (questPlayerHandler.quest.QuestTitle == QuestTitleToCompare && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCompare)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
