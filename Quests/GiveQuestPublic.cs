using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GiveQuestPublic : MonoBehaviour
{
    [SerializeField] QuestDefiner QuestToGive;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] int SceneToLoad;
    [SerializeField] int SpawnToSpawn;

    public void GiveQuest()
    {
        questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        QuestToGive.IsQuestActive = true;
        questPlayerHandler.quest = QuestToGive;
        Destroy(this);
    }

    public void TransportarAEscena()
    {
        
        BetweenScenes player = FindObjectOfType<BetweenScenes>();
        player.SpawnToTake = SpawnToSpawn;
        SceneManager.LoadScene(SceneToLoad);

    }
}
