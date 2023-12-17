using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segurata_Behaviour : Enemy
{
    [SerializeField] bool IsActive;
    [SerializeField] DialoguesThroughSequences[] dialogues;


    void Start()
    {
        ActualHealth = MaxHealth;
        IsEnemyAlive = true;
        IsActive = true;
    }

    private void Update()
    {
        if (ActualHealth <= 0 && IsEnemyAlive)
        {
            OnDie();
        }
    }

    private void OnDie() //Morir i donar per acabada la missio
    {
        IsEnemyAlive = false;
        dialogues[1].enabled= true;
        dialogues[1].CridarStartDialogue();
    }

}
