using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EscollirBossFinal : MonoBehaviour
{
    [SerializeField] GameObject QuickTimeEvent_Gnl;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] IvaaBossController ivaaController;
    [SerializeField] QuestPlayerHandler questHandler;
    [SerializeField] PlayerController playerController;
    [SerializeField] string QuestTitleToCheck;
    [SerializeField] int QuestProgressionToCheck;


    [Header("Barnel")]
    [SerializeField] BarnelBoss_Behaviour barnelBoss_Behaviour;
    [SerializeField] Collider2D barnelDialogueCollider;
    [SerializeField] Collider2D barnelBattleCollider;

    private void Start()
    {
        questHandler = FindObjectOfType<QuestPlayerHandler>();
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            Debug.Log("Escull Ivaa");
            playableDirector.Stop();
            ivaaController.enabled = true;
            ivaaController.IsEnemyAlive = true;
            ivaaController.IsActive = true;
            
            ivaaController.ActualHealth = ivaaController.MaxHealth;
            if (questHandler.quest.QuestTitle == QuestTitleToCheck && questHandler.quest.QuestPorgression == QuestProgressionToCheck)
            {
                Debug.Log("Avanca la missio " + QuestTitleToCheck + " GameObject:" + gameObject.name);
                questHandler.quest.QuestPorgression++;
            }
            ComprovarSiShaDeGirar();

            QuickTimeEvent_Gnl.SetActive(false);
        }
        else if(Input.GetKeyDown("e"))
        {
            Debug.Log("Escull Barnel");
            playableDirector.Stop();
            barnelBoss_Behaviour.enabled = true;
            barnelDialogueCollider.enabled = false;
            barnelBattleCollider.enabled = true;
            if (questHandler.quest.QuestTitle == QuestTitleToCheck && questHandler.quest.QuestPorgression == QuestProgressionToCheck)
            {
                Debug.Log("Avanca la missio " + QuestTitleToCheck + " GameObject:" + gameObject.name);
                questHandler.quest.QuestPorgression++;
            }
            ComprovarSiShaDeGirar();
            QuickTimeEvent_Gnl.SetActive(false);
        }
    }
    private void ComprovarSiShaDeGirar()
    {
        if (playerController.isFacingRight && playerController.gameObject.transform.localScale.x == -1)
        {
            Vector3 localScale = playerController.gameObject.transform.localScale;
            localScale.x *= -1f;
            playerController.gameObject.transform.localScale = localScale;
        }
        else if (!playerController.isFacingRight && playerController.gameObject.transform.localScale.x == 1)
        {
            Vector3 localScale = playerController.gameObject.transform.localScale;
            localScale.x *= -1f;
            playerController.gameObject.transform.localScale = localScale;
        }
    }
}
