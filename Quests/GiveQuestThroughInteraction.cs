using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveQuestThroughInteraction : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] GameObject Player;
    [SerializeField] QuestDefiner QuestToGive;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] string QuestNameToCompare;

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
            if (Input.GetKey("e") && questPlayerHandler.quest.QuestTitle==QuestNameToCompare)
            {
                QuestToGive.IsQuestActive = true;
                questPlayerHandler.quest = QuestToGive;
                this.enabled = false;
                this.hideFlags = HideFlags.HideInInspector;
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
