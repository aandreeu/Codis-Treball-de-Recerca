using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class HomeUniforme_Behaviour : MonoBehaviour
{
    [SerializeField] string[] QuestTitles;
    [SerializeField] int[] QuestProgressions;
    [SerializeField] QuestPlayerHandler playerHandler;
    [SerializeField] PlayerInventory playerInventory;

    [SerializeField] Dialogues[] dialogues;
    [SerializeField] AdvanceQuestThroughDialogue[] advanceQuestThroughDialogues;
    [SerializeField] PlaySequenceThroughDialogue[] playSequenceThroughDialogues;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] TimelineAsset[] timelineAssets;
    [SerializeField] GameObject Home_GO;
    [SerializeField] SceneLoaderForPlayer sceneLoader;

    private void OnEnable()
    {
        playerHandler = FindObjectOfType<QuestPlayerHandler>();
        playerInventory=FindObjectOfType<PlayerInventory>();
        if (playerHandler.quest.QuestTitle == QuestTitles[0])
        {
            Home_GO.SetActive(true);
            if (playerHandler.quest.QuestPorgression == QuestProgressions[0])
            {
                playableDirector.playableAsset = timelineAssets[0];
                playSequenceThroughDialogues[0].enabled= true;
                playSequenceThroughDialogues[1].enabled= false;
                dialogues[0].enabled = true;
                advanceQuestThroughDialogues[0].enabled = true;
                dialogues[1].enabled = false;
                advanceQuestThroughDialogues[1].enabled = false;
                sceneLoader.CanPlayerTp = false;
            }
            else if (playerHandler.quest.QuestPorgression == QuestProgressions[1] && playerInventory.CurrentMoney>=150f)
            {
                playSequenceThroughDialogues[0].enabled = false;
                playSequenceThroughDialogues[1].enabled = true;
                playableDirector.playableAsset = timelineAssets[1];
                dialogues[0].enabled = false;
                advanceQuestThroughDialogues[0].enabled = false;
                dialogues[1].enabled = true;
                advanceQuestThroughDialogues[1].enabled = true;
            }
            else
            {
                playSequenceThroughDialogues[0].enabled = false;
                playSequenceThroughDialogues[1].enabled = false;
                dialogues[0].enabled = false;
                advanceQuestThroughDialogues[0].enabled = false;
                dialogues[1].enabled = false;
                dialogues[2].enabled = true;
                advanceQuestThroughDialogues[1].enabled = false;
            }
        }
        else
        {
            Home_GO.SetActive(false);
            sceneLoader.CanPlayerTp = true;
        }
    }

    //public void DefineGameObjectInTimeline(string SteamNameToCompare, GameObject gameObjectToDefine)
    //{
    //    foreach (var playableAsset in playableDirector.playableAsset.outputs)
    //    {
    //        if (playableAsset.streamName == SteamNameToCompare)
    //        {
    //            playableDirector.SetGenericBinding(playableAsset.sourceObject, gameObjectToDefine);
    //        }
    //    }
    //}

    public void SubstractAllPlayersMoney()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        float moneyToSubstract = playerInventory.CurrentMoney;
        playerInventory.ManipulateMoney(-moneyToSubstract);
        sceneLoader.CanPlayerTp = true; //Ho crido aqui pq abans era a OnEnable i no es comprovava despres del dialeg
    }
}
