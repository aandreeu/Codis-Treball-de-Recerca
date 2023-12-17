using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalSequenceController : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] GameObject Player;
    [SerializeField] string SteamNameToCompare;
    [SerializeField] DialoguesThroughSequences dialoguesThroughSequences;
    [SerializeField] int indexToCompare;
    [SerializeField] GameObject QuickTimeEvent;
    [SerializeField] bool isQTEActive;
    [SerializeField] bool HasPlayerPicked;

    private void Start()
    {
        Player = GameObject.Find("Player");
        DefinePlayerInTimeline(SteamNameToCompare, Player);
    }
    private void Update()
    {
        if (dialoguesThroughSequences.lineIndex==indexToCompare && !isQTEActive)
        {
            QuickTimeEvent.SetActive(true);
            isQTEActive= true;
        }

        if(isQTEActive && Input.GetKeyDown("e") && !HasPlayerPicked)
        {
            Debug.Log("Barnel");
            HasPlayerPicked = true;
            QuickTimeEvent.SetActive(false);
        }
        else if(isQTEActive && Input.GetKeyDown("q") && !HasPlayerPicked)
        {
            Debug.Log("Ivaa");
            HasPlayerPicked = true;
            QuickTimeEvent.SetActive(false);
        }
    }
    private void DefinePlayerInTimeline(string SteamNameToCompare, GameObject gameObjectToDefine)
    {
        foreach (var playableAsset in playableDirector.playableAsset.outputs)
        {
            if (playableAsset.streamName == SteamNameToCompare)
            {
                playableDirector.SetGenericBinding(playableAsset.sourceObject, gameObjectToDefine);
            }
        }
    }
}
