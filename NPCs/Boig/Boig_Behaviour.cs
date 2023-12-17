using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Boig_Behaviour : MonoBehaviour
{
    [SerializeField] string[] QuestTitle;
    [SerializeField] int[] QuestProgression;
    public bool HasBattleStarted;
    [SerializeField] EnemySpawner[] enemySpawners;
    [SerializeField] GameObject Boig_GO;
    public QuestPlayerHandler questPlayerHandler;
    [SerializeField] PlayableDirector playableDirector;

    private void Start()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        HasBattleStarted = false;
    }
    private void Update()
    {
        if (QuestProgression[0] == questPlayerHandler.quest.QuestPorgression && QuestTitle[0] == questPlayerHandler.quest.QuestTitle)
        {
            Boig_GO.SetActive(true);
        }
        else if (QuestProgression[1] == questPlayerHandler.quest.QuestPorgression && QuestTitle[1] == questPlayerHandler.quest.QuestTitle)
        {
            Boig_GO.SetActive(true);
        }

        if (HasBattleStarted && enemySpawners[0].EnemiesLeftToSpawn==0)
        {
            Debug.Log("Acaba la batalla");
            HasBattleStarted= false;
            playableDirector.Play();
            questPlayerHandler.AdvanceQuestPhase();
        }

    }

}
