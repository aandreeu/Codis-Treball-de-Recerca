using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeKoQuestManager : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject[] QuestGameObjects;
    [SerializeField] string[] QuestTitles;
    [SerializeField] int[] QuestProgressionToCompare;
    [SerializeField] QuestPlayerHandler playerHandler;
    #endregion
    private void OnEnable() //Es crida quan es carrega l escena.
    {
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (playerHandler.quest.QuestTitle == QuestTitles[0] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[0])
        {
            Debug.Log("Aras");
            QuestGameObjects[0].SetActive(true);
            QuestGameObjects[1].SetActive(false);
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[1] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[1])
        {
            Debug.Log("Amic");
            QuestGameObjects[0].SetActive(false);
            QuestGameObjects[1].SetActive(true);
        }
        else
        {
            Debug.Log("Res");
            QuestGameObjects[0].SetActive(false);
            QuestGameObjects[1].SetActive(false);
        }
    }
}
