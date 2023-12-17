using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ReadyToFight_Lab_Behaviour : MonoBehaviour
{

    public TimelineAsset[] timelines;
    public PlayableDirector playableDirector;
    [SerializeField] QuestPlayerHandler playerHandler;
    [SerializeField] AdvanceQuestThroughDialogue[] advanceQuestThroughDialogue;
    [SerializeField] string[] QuestTitles;

    private void OnTriggerEnter2D() //Es crida quan es carrega l escena.
    {
        Debug.Log("Chekin");
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (playerHandler.quest.QuestTitle == QuestTitles[0] && playerHandler.quest.QuestPorgression==3)
        {
            playableDirector.playableAsset = timelines[0];
            advanceQuestThroughDialogue[0].enabled = false;
            advanceQuestThroughDialogue[1].enabled = false;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[1])
        {
            playableDirector.playableAsset = timelines[1];
            advanceQuestThroughDialogue[0].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[2])
        {
            playableDirector.playableAsset = timelines[2];
            advanceQuestThroughDialogue[1].enabled = true;
            advanceQuestThroughDialogue[0].enabled = false;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[3])
        {
            playableDirector.playableAsset = timelines[3];
            advanceQuestThroughDialogue[1].enabled = false;
            advanceQuestThroughDialogue[0].enabled = false;
            advanceQuestThroughDialogue[2].enabled = true;
        }
        else if (playerHandler.quest.QuestTitle == QuestTitles[4])
        {
            playableDirector.playableAsset = timelines[4];
            advanceQuestThroughDialogue[1].enabled = false;
            advanceQuestThroughDialogue[0].enabled = false;
            advanceQuestThroughDialogue[2].enabled = true;
            advanceQuestThroughDialogue[3].enabled = true;
        }

    }

    public void FreezePlayer()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        player.GetComponent<PlayerController>().FreezePlayer();
    }
    public void UnfreezePlayer()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        player.GetComponent<PlayerController>().UnfreezePlayer();
        player.GetComponent<PlayerController>().IsSwordOut=true;
        player.GetComponent<Animator>().Play("TreureEspasa_Player");
        player.GetComponent<Animator>().SetBool("EspasaTreta",true);
    }

}
