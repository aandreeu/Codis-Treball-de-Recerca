using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vell_Behaviour : MonoBehaviour
{
    [SerializeField] TriggerMinigame triggerMinigame;
    [SerializeField] Dialogues[] dialogues;
    private void OnEnable()
    {
        QuestPlayerHandler playerHandler=FindObjectOfType<QuestPlayerHandler>();
        if (playerHandler.quest.QuestTitle=="Indici de llum." && playerHandler.quest.QuestPorgression==6)
        {
            triggerMinigame.enabled = false;
            dialogues[0].enabled= true;
        }
    }

}
