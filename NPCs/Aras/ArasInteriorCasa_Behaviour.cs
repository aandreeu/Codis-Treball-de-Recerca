using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArasInteriorCasa_Behaviour : MonoBehaviour
{
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] string[] QuestTitleToCompare;
    [SerializeField] int[] QuesProgressionToCompare;

    [SerializeField] GameObject Aras_GO;
    [SerializeField] Dialogues[] dialogues;
    [SerializeField] SleepInBed sleepInBed;
    [SerializeField] GameObject Bed_Press_E;
    [SerializeField] GameObject Aras_Press_E;

    private void OnEnable()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        dialogues[0].enabled = false;
        dialogues[1].enabled = false;
        dialogues[2].enabled = false;
        dialogues[3].enabled = false;
        Bed_Press_E.SetActive(false);
        sleepInBed.CanSleep = false;
        Aras_GO.SetActive(true);
        Aras_Press_E.SetActive(true);

        if (questPlayerHandler.quest.QuestTitle == QuestTitleToCompare[0] && questPlayerHandler.quest.QuestPorgression == QuesProgressionToCompare[0])
        {
            dialogues[0].enabled= true;
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitleToCompare[1] && questPlayerHandler.quest.QuestPorgression == QuesProgressionToCompare[1])
        {
            dialogues[2].enabled= true;
        }
        else if (questPlayerHandler.quest.QuestTitle == QuestTitleToCompare[2] && questPlayerHandler.quest.QuestPorgression == QuesProgressionToCompare[2])
        {
            dialogues[3].enabled= true;
        }
        else
        {
            Aras_GO.SetActive(false);
            Aras_Press_E.SetActive(false);
            Bed_Press_E.SetActive(true);
            sleepInBed.CanSleep = true;
        }
    }
}
