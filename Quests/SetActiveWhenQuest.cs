using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveWhenQuest : MonoBehaviour
{
    [Header("Quest Things")]
    [SerializeField] QuestPlayerHandler playerHandler;
    [SerializeField] string[] QuestTitleToCompare;
    [SerializeField] int[] QuestProgressionToCompare;
    [SerializeField] bool SetActive;
    [Header("Components de la missio")]
    [SerializeField] GameObject[] GameObjectsToEnable;

    void Start()
    {
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        foreach (var Go in GameObjectsToEnable)
        {
            Go.SetActive(!SetActive);
        }
    }

    void Update()
    {
        if (playerHandler.quest.QuestTitle == QuestTitleToCompare[0] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[0])
        {
            foreach(var Go in GameObjectsToEnable)
            {
                Go.SetActive(SetActive);
            }
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[1] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[1])
        {
            foreach (var Go in GameObjectsToEnable)
            {
                Go.SetActive(SetActive);
            }
        }
        else if (playerHandler.quest.QuestTitle == QuestTitleToCompare[2] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[2])
        {
            foreach (var Go in GameObjectsToEnable)
            {
                Go.SetActive(SetActive);
            }
        }
        else
        {
            foreach (var Go in GameObjectsToEnable)
            {
                Go.SetActive(false);
            }
        }
    }
}
