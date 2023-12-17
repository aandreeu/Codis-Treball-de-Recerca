using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveQuestOnCollide : MonoBehaviour
{
    [SerializeField] QuestDefiner QuestToGive;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] string QuestNameToCompare;
    [SerializeField] int QuestProgressionToCompare;

    void Start()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si entra al rang dona la missio
    {
        if (collision.CompareTag("Player") && questPlayerHandler.quest.QuestTitle==QuestNameToCompare && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCompare)
        {
            QuestToGive.IsQuestActive = true;
            questPlayerHandler.quest = QuestToGive;
            Destroy(this);
        }
    }
}
