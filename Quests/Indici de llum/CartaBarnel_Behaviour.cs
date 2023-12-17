using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaBarnel_Behaviour : MonoBehaviour
{
    [SerializeField] GameObject Carta_Gnl;
    [SerializeField] QuestPlayerHandler questPlayerHandler;

    private void OnEnable()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (questPlayerHandler.quest.QuestTitle=="Indici de llum." && questPlayerHandler.quest.QuestPorgression==0)
        {
            Carta_Gnl.SetActive(true);
        }
        else
        {
            Carta_Gnl.SetActive(false);
        }
    }
}
