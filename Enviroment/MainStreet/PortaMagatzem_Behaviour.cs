using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaMagatzem_Behaviour : MonoBehaviour
{
    [SerializeField] Dialogues dialogues;
    [SerializeField] PlaySequenceThroughDialogue playSequenceThroughDialogue;
    [SerializeField] string QuestTitleToCompare;
    [SerializeField] int QuestProgressionToCompare;

    private void OnEnable()
    {
        QuestPlayerHandler questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (questPlayerHandler.quest.QuestPorgression == QuestProgressionToCompare && questPlayerHandler.quest.QuestTitle == QuestTitleToCompare)
        {
            dialogues.enabled=true;
            playSequenceThroughDialogue.enabled = true;
        }
        else
        {
            dialogues.enabled = false;
            playSequenceThroughDialogue.enabled = false;
        }
    }

}
