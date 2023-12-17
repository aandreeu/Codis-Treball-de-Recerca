using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetterMGManager : MonoBehaviour
{

    public int[] SearchedWord;
    public int[] RecievedWord;
    [SerializeField] PlayerController playerController;
    [SerializeField] Dialogues[] dialogues;
    [SerializeField] PlayerHudController playerHudController;
    [SerializeField] PublicAdvanceQuest advanceQuest;

    public bool IsGamePlaying;

    void Start()
    {
        IsGamePlaying = true; 
    }

    void Update()
    {
        if (RecievedWord.SequenceEqual(SearchedWord))
        {
            Debug.Log("Iwal");

            IsGamePlaying= false;
            playerController = FindObjectOfType<PlayerController>();
            playerHudController = FindObjectOfType<PlayerHudController>();
            playerController.UnfreezePlayer();
            playerHudController.AbleOpenMenu();
            advanceQuest.AdvanceQuest();
            dialogues[0].enabled= false;
            dialogues[1].enabled= true;
            Invoke("EndMinigame", 2f);
        }
    }

    public void EndMinigame()
    {
        gameObject.SetActive(false);
    }
}
