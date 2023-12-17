using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnInteractStartAgain : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerController playerController;
    [SerializeField] Animator ObjectAnimator;
    [SerializeField] string[] AnimationsToUse;
    [SerializeField] bool AnimationPhase;
    [SerializeField] bool IsAnimating;

    void Start()
    {
        IsPlayerInRange = false;
        IsAnimating = false;
        AnimationPhase = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = FindObjectOfType<PlayerController>();
        if (ObjectAnimator == null)
        {
            ObjectAnimator = gameObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && IsPlayerInRange && !IsAnimating)
        {
            if (!AnimationPhase)
            {
                ObjectAnimator.Play(AnimationsToUse[0]);
                AnimationPhaseTrue();
            }
            else
            {
                ObjectAnimator.Play(AnimationsToUse[1]);
                AnimationPhaseFalse();
            }

        }
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

    public void IsAnimatingTrue()
    {
        IsAnimating = true;
    }
    public void IsAnimatingFalse()
    {
        IsAnimating = false;
    }

    public void AnimationPhaseTrue()
    {
        AnimationPhase = true;
    }
    public void AnimationPhaseFalse()
    {
        AnimationPhase = false;
    }
}

