using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstacioBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] SetActiveOnInteract interact;
    [SerializeField] string[] QuestTitlesThatBan;
    [SerializeField] QuestPlayerHandler playerHandler;
    #endregion
    private void OnEnable() //Es crida quan es carrega l escena.
    {
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (playerHandler.quest.QuestTitle == QuestTitlesThatBan[0])
        {
            interact.enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            interact.enabled=true;
        }
    }
}
