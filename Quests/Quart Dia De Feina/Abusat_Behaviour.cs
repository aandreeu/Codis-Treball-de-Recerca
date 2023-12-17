using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abusat_Behaviour : MonoBehaviour
{
    [SerializeField] Animator abusatAnimator;
    [SerializeField] QuestPlayerHandler questPlayerHandler;
    [SerializeField] bool Done;
    [SerializeField] BoxCollider2D colliderEnemic;
    [SerializeField] BoxCollider2D colliderEnemic2;
    void Start()
    {
        questPlayerHandler=FindObjectOfType<QuestPlayerHandler>();
    }

    void Update()
    {
        if (colliderEnemic.enabled==false && colliderEnemic2.enabled == false && questPlayerHandler.quest.QuestTitle=="Quart dia de feina." && questPlayerHandler.quest.QuestPorgression == 10 && !Done)
        {
            abusatAnimator.Play("AixecarSe_Abusat");
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            Done= true;
            SceneLoaderForPlayer[] tps = FindObjectsOfType<SceneLoaderForPlayer>();
            foreach (var tp in tps)
            {
                tp.CanPlayerTp = true;
            }
            this.enabled= false;
        }
    }
}
