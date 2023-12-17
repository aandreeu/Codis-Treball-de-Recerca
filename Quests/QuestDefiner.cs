using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestDefiner
{
    public bool IsQuestActive;
    public string QuestTitle;
    [TextArea(8,6)]
    public string QuestDescription;
    public string[] QuestPhases;
    public int QuestPorgression;
    public int MoneyReward;
    public QuestGoal QuestGoal;

    public void Complete()
    {
        IsQuestActive= false;
        Debug.Log("Finished");
    }
}
