using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class CheckerEntradaSalaControl_Behaviour : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] GameObject Player;
    [SerializeField] string SteamNameToCompare;

    private void Start()
    {
        Player=GameObject.Find("Player");
        DefineGameObjectInTimeline(SteamNameToCompare,Player);
    }

    public void DefineGameObjectInTimeline(string SteamNameToCompare, GameObject gameObjectToDefine)
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
