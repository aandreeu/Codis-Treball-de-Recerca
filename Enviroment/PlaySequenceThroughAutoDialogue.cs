using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlaySequenceThroughAutoDialogue : MonoBehaviour
{
    public PlayableDirector[] playableDirector;
    [SerializeField] AutoTriggeableDialogues autoTriggeableDialogues;
    [SerializeField] int dialogueLineToPlay;
    public int SequenceToPlay;

    private void Update()
    {
        if (autoTriggeableDialogues.lineIndex == dialogueLineToPlay)
        {
            playableDirector[SequenceToPlay].Play();
            this.enabled = false;
        }
    }

}
