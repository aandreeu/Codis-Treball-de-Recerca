using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArasExtCasa_Behaviour : MonoBehaviour
{

    [SerializeField] GameObject Aras;
    [SerializeField] GameObject pressE;

    [SerializeField] string[] QuestTitlesToCompare;
    [SerializeField] int[] QuestProgressionsToCompare;
    [SerializeField] QuestPlayerHandler questPlayerHandler;

    private void OnEnable()
    {
        questPlayerHandler=FindObjectOfType<QuestPlayerHandler>();
        if (questPlayerHandler.quest.QuestTitle == QuestTitlesToCompare[0] && questPlayerHandler.quest.QuestPorgression == QuestProgressionsToCompare[0]){
            Aras.SetActive(true);
            Aras.GetComponent<Dialogues>().enabled= true;
            pressE.SetActive(true);
        }
        else
        {
            Aras.SetActive(false);
            pressE.SetActive(false);
        }
    }
}
