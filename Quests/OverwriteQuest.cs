using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteQuest : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] GameObject Player;
    [SerializeField] QuestDefiner QuestToGive;
    [SerializeField] QuestPlayerHandler questPlayerHandler;

    void Start()
    {
        IsPlayerInRange = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
    }

    void Update()
    {
        if (IsPlayerInRange)
        {
            if (Input.GetKey("e"))
            {
                QuestToGive.IsQuestActive = true;
                questPlayerHandler.quest = QuestToGive;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //Si entra al rang, es activa IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Si surt del rang, es desactiva IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }
}
