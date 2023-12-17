using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public GameObject[] bosses;
    [SerializeField] string[] QuestTitlesToCompare;
    [SerializeField] QuestPlayerHandler playerHandler;


    private void OnEnable() //Es crida quan es carrega l escena.
    {
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        bosses[0].SetActive(false);
        bosses[1].SetActive(false);
        bosses[2].SetActive(false);
        bosses[3].SetActive(false);
        bosses[4].SetActive(false);
        if (playerHandler.quest.QuestTitle == QuestTitlesToCompare[0])
        {
            bosses[0].SetActive(true);
        }
        else if (playerHandler.quest.QuestTitle == QuestTitlesToCompare[1])
        {
            bosses[1].SetActive(true);
        }
        else if (playerHandler.quest.QuestTitle == QuestTitlesToCompare[2])
        {
            bosses[2].SetActive(true);
        }
        else if (playerHandler.quest.QuestTitle == QuestTitlesToCompare[3])
        {
            bosses[3].SetActive(true);
        }
        else if (playerHandler.quest.QuestTitle == QuestTitlesToCompare[4])
        {
            bosses[4].SetActive(true);
        }

    }
}
