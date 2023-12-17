using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlaySequenceThroughSequenceDialogue : MonoBehaviour
{
    public PlayableDirector playableDirector;
    [SerializeField] TimelineAsset timeline;
    [SerializeField] DialoguesThroughSequences dialogueThroughSequence;
    [SerializeField] int dialogueLineToPlay;

    private void Update()
    {
        if (dialogueThroughSequence.lineIndex == dialogueLineToPlay)
        {
            playableDirector.playableAsset=timeline;
            playableDirector.Play();
            this.enabled = false;
        }
    }

}
