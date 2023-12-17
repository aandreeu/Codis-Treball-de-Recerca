using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnInteract : MonoBehaviour
{
    public AnimatorAnimation[] animations;
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] bool CanPlayIt;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerController playerController;
    [SerializeField] string AnimationToUseInPlayer;
    [SerializeField] float TimeToDoItAgain;

    void Start()
    {
        IsPlayerInRange = false;
        CanPlayIt = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && IsPlayerInRange && CanPlayIt)
        {
            foreach (AnimatorAnimation anim in animations)
            {
                anim.animator.Play(anim.animationName);
            }
            if(AnimationToUseInPlayer!= null)
            {
                Player.GetComponent<Animator>().Play(AnimationToUseInPlayer);
            }
            CanPlayIt= false;
            Invoke("CanPlayItTrue", TimeToDoItAgain);
        }
    }

    void CanPlayItTrue()
    {
        CanPlayIt = true;
    }
    private void OnTriggerEnter2D(Collider2D collision) //Si entra al rang, s activa IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Si surt del rang, es desactiva IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }

}
