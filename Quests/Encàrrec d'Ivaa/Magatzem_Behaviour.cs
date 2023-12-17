using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Magatzem_Behaviour : MonoBehaviour
{
    [Header("Mission Things")]
    [SerializeField] Dialogues[] Dialogues;
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] CapsuleCollider2D boxCollider;
    [Header("Player")]
    [SerializeField] QuestPlayerHandler playerHandler;
    [Header("Quests Data")]
    [SerializeField] string[] QuestTitleToCompare;
    [SerializeField] int[] QuestProgressionToCompare;
    public void OnEnable()
    {
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        gameObjects[0].GetComponent<BoxCollider2D>().enabled = false;
        if (playerHandler.quest.QuestTitle == QuestTitleToCompare[0] && playerHandler.quest.QuestPorgression == QuestProgressionToCompare[0])
        {
            Debug.Log("Si");
            Dialogues[0].enabled = true;
            boxCollider.enabled = true;
        }
        else if(playerHandler.quest.QuestTitle != QuestTitleToCompare[0] || playerHandler.quest.QuestPorgression != QuestProgressionToCompare[0])
        {
            Debug.Log("No");
            boxCollider.enabled = false;

            foreach (Dialogues d in Dialogues)
            {
                d.enabled = false;
            }
            foreach (GameObject go in gameObjects)
            {
                go.SetActive(false);
            }
        }
    }


}
