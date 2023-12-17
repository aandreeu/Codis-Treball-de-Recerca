using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public QuestPlayerHandler questPlayerHandle;
    [SerializeField] BoxCollider2D ADphase;
    [SerializeField] GameObject ADphaseTXT;
    [SerializeField] int TutorialPhase;
    [SerializeField] string QuestTitleToCompare;

    public QuestDefiner quest;

    void Start()
    {
        questPlayerHandle=FindObjectOfType<QuestPlayerHandler>();
        ADphase.enabled = true;
        ADphaseTXT.SetActive(true); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(TutorialPhase == 0 && questPlayerHandle.quest.QuestPorgression==0 && questPlayerHandle.quest.QuestTitle==QuestTitleToCompare) //Arriba a x punt caminant
        {
            questPlayerHandle.AdvanceQuestPhase();
            TutorialPhase++;
            ADphase.enabled = false;
            ADphaseTXT.SetActive(false);
        }
        //else if(TutorialPhase == 1)
        //{
        //    questPlayerHandle.AdvanceQuestPhase();
        //    TutorialPhase++;
        //    quest.IsQuestActive = true;
        //    questPlayerHandle.quest = quest;
        //}

    }
}
