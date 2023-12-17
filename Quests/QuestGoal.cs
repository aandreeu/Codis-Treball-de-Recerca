using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public QuestType questType;

    public bool IsReached;

    public enum QuestType
    {
        Kill,
        GoTo,
        Buy
    }
}
