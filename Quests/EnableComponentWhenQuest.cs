using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponentWhenQuest : MonoBehaviour
{
    [Header("Quest Things")]
    [SerializeField] QuestPlayerHandler playerHandler;
    [SerializeField] string[] QuestTitleToCompare;
    [SerializeField] int[] QuestProgressionToCompare;
    [Header("Components de la missio")]
    [SerializeField] Dialogues[] DialoguesToEnable;
    [SerializeField] AdvanceQuestThroughDialogue[] AdvanceQuestToEnable;
    int i = 0;

    private void Start()
    {
        playerHandler=FindObjectOfType<QuestPlayerHandler>();
    }

    void Update()
    {
        while (i <= QuestTitleToCompare.Length)
        {
            if (playerHandler.quest.QuestTitle == QuestTitleToCompare[i] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[i])
            {
                foreach (var dialoges in DialoguesToEnable)
                {
                    dialoges.enabled = false;
                }
                DialoguesToEnable[i].enabled = true; 
                foreach (var advanceQ in AdvanceQuestToEnable)
                {
                    advanceQ.enabled = false;
                }
                AdvanceQuestToEnable[i].enabled = true;
                i = 0; break;
            }
            i++;
        }
        i = 0;
    }
}
