using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DefineSequenceSteam : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] GameObject[] ObjectsToDefine;
    [SerializeField] string[] SteamNameToCompare;

    [SerializeField] bool Player;
    [SerializeField] bool MainCamera;

    private void Start()
    {
        if (MainCamera){
            ObjectsToDefine[1] = GameObject.Find("CM vcam1");
        }
        if (Player){
            ObjectsToDefine[0] = GameObject.Find("Player");
        }

        int i = 0;
        foreach (GameObject go in ObjectsToDefine)
        {
            DefineGameObjectsInTimeline(SteamNameToCompare[i], go);
            i++;
        }
    }

    public void DefineGameObjectsInTimeline(string SteamNameToCompare, GameObject gameObjectToDefine)
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
