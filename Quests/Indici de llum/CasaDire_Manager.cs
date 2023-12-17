using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CasaDire_Manager : MonoBehaviour
{
    [SerializeField] bool CanCall;
    [SerializeField] bool Cridat;
    [SerializeField] bool Cridat1;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] TimelineAsset[] timelines;
    [SerializeField] GameObject playerGO;
    [SerializeField] DialoguesThroughSequences dialoguessequences;

    private void Start()
    {
        CanCall = true;
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        playerGO=questPlayerHandler.gameObject;
    }
    private void Update()
    {
        if (!CanCall && dialoguessequences.enabled==false && !Cridat)
        {
            CanCall = true;
            Cridat = true;
        }


        if(!Cridat1 && CanCall && questPlayerHandler.quest.QuestTitle=="Indici de llum." && questPlayerHandler.quest.QuestPorgression == 4)
        {
            PlayDirector(timelines[0]);
            Cridat1 = true;
        }
        if(CanCall && questPlayerHandler.quest.QuestTitle=="Indici de llum." && questPlayerHandler.quest.QuestPorgression == 5)
        {
            PlayDirector(timelines[1]);
        }
    }
    private void PlayDirector(TimelineAsset tl)
    {
        playableDirector.playableAsset = tl;
        foreach (var playableAsset in playableDirector.playableAsset.outputs)
        {
            if (playableAsset.streamName == "Player")
            {
                playableDirector.SetGenericBinding(playableAsset.sourceObject, playerGO);
            }
        }
        playerGO.GetComponent<PlayerController>().FreezePlayer();
        playableDirector.Play();
        CanCall=false;
    }
}
