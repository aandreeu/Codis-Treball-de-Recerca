using UnityEngine;
using UnityEngine.Playables;

public class PlaySequence : MonoBehaviour
{
    [SerializeField] string QuestTitle;
    [SerializeField] int QuestProgression;
    [SerializeField] bool HasPlayed;
    public QuestPlayerHandler questPlayerHandler;
    public PlayableDirector[] playableDirector;
    public int SequenceToPlay;

    private void OnTriggerEnter2D(Collider2D collision) //Si entra al rang, s activa IsPlayerInRange
    {
        questPlayerHandler=FindObjectOfType<QuestPlayerHandler>();
        if (!HasPlayed && QuestProgression == questPlayerHandler.quest.QuestPorgression && QuestTitle == questPlayerHandler.quest.QuestTitle && collision.CompareTag("Player"))
        {
            playableDirector[SequenceToPlay].Play();
            HasPlayed=true;

        }

    }
}
