using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa_Behaviour : MonoBehaviour
{
    [SerializeField] BoxCollider2D ColliderE;
    [SerializeField] string QuestTitleToCompare;
    [SerializeField] int QuestProgressionToCompare;
    private void OnEnable()
    {
        QuestPlayerHandler playerHandler = FindObjectOfType<QuestPlayerHandler>();
        ColliderE= gameObject.GetComponent<BoxCollider2D>();
        if (playerHandler.quest.QuestTitle == QuestTitleToCompare && playerHandler.quest.QuestPorgression == QuestProgressionToCompare)
        {
            ColliderE.enabled= true;
        }
        else
        {
            ColliderE.enabled = false;
        }

    }

}
