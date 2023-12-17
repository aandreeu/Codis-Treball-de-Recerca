using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceQuestNoCompare : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] bool CanRequest;
    public QuestPlayerHandler questHandler;

    private void Start()
    {
        questHandler = FindObjectOfType<QuestPlayerHandler>();
        IsPlayerInRange = false;
        CanRequest = true;
    }

    private void Update()
    {
        if (IsPlayerInRange)
        {
            if (Input.GetKey("e") && CanRequest)
            {
                questHandler.quest.QuestPorgression++;
                CanRequest= false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //Si el tipus de fase es d anar a algun lloc, al topar amb aquesta barrera invisible cridem a advanceMission
    {        
        IsPlayerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsPlayerInRange = false;
        CanRequest = true;
    }
}
