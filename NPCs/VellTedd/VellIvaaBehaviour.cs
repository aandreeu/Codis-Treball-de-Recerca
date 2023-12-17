using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VellIvaaBehaviour : MonoBehaviour
{

    [SerializeField] GameObject Ivaa_Gnl;
    [SerializeField] GameObject Vell_Gnl;
    [SerializeField] string QuestTitleToCompare;
    [SerializeField] int QuestProgressionToCompare;

    private void OnEnable()
    {
        QuestPlayerHandler questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (questPlayerHandler.quest.QuestPorgression == QuestProgressionToCompare && questPlayerHandler.quest.QuestTitle == QuestTitleToCompare)
        {
            Ivaa_Gnl.SetActive(true);
            Vell_Gnl.SetActive(true);
        }
        else
        {
            Ivaa_Gnl.SetActive(false);
            Vell_Gnl.SetActive(false);
        }
    }

}
