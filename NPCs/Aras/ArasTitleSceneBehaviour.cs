using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArasTitleSceneBehaviour : MonoBehaviour
{
    [SerializeField] string QuestTitleToCompare;
    [SerializeField] int QuestProgressionToCompare;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] BoxCollider2D colliderDial;

    private void Update()
    {
        if (questPlayerHandler.quest.QuestTitle == QuestTitleToCompare && questPlayerHandler.quest.QuestPorgression == QuestProgressionToCompare + 1)
        {
            colliderDial.enabled = false;
        }
    }
}
