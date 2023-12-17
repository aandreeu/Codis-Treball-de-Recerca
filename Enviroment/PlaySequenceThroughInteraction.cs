using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static AdvanceQuestProgression;

public class PlaySequenceThroughInteraction : MonoBehaviour
{
    [SerializeField] string QuestTitle;
    [SerializeField] int QuestProgression;
    [SerializeField] bool HasPlayed;
    public QuestPlayerHandler questPlayerHandler;
    [SerializeField] TimelineAsset timeline;
    public PlayableDirector[] playableDirector;
    public int SequenceToPlay;
    [SerializeField] bool IsPlayerInRange;
    private void Start()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        IsPlayerInRange = false;
    }

    private void Update()
    {
        if (IsPlayerInRange)
        {
            if (Input.GetKey("e") && questPlayerHandler.quest.QuestTitle==QuestTitle && questPlayerHandler.quest.QuestPorgression==QuestProgression && !HasPlayed)
            {
                playableDirector[SequenceToPlay].playableAsset = timeline;
                playableDirector[SequenceToPlay].Play();
                HasPlayed = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si el tipus de fase es d anar a algun lloc, al topar amb aquesta barrera invisible cridem a advanceMission
    {
        IsPlayerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsPlayerInRange = false;
    }
}
