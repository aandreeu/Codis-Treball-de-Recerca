using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlaySequenceThroughDialogue : MonoBehaviour
{
    [SerializeField] bool CanBePlayedAgain;
    public PlayableDirector[] playableDirector;
    [SerializeField] Dialogues dialogue;
    [SerializeField] int dialogueLineToPlay;
    public int SequenceToPlay;

    private void Update() 
    {
        if (dialogue.lineIndex==dialogueLineToPlay)
        {
            playableDirector[SequenceToPlay].Play();
            this.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Si surt del rang, es desactiva IsPlayerInRange
    {
        if(CanBePlayedAgain)
        {
            this.enabled = true;
        }
    }
}
