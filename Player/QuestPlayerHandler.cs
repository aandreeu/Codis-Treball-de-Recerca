using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlayerHandler : MonoBehaviour
{
    public QuestDefiner quest;


    public void AdvanceQuestPhase() //Cridar quan s'arriba a algun punt especific de la missio, per avancar la mateixa
    {
        quest.QuestPorgression += 1;
    }
}
