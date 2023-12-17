using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnInteract : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerController playerController;
    [SerializeField] Animator ObjectAnimator;
    [SerializeField] string AnimationToUse;

    void Start()
    {
        IsPlayerInRange = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = FindObjectOfType<PlayerController>();
        if (ObjectAnimator == null)
        {
            ObjectAnimator = gameObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && IsPlayerInRange)
        {
            ObjectAnimator.Play(AnimationToUse);
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
}

