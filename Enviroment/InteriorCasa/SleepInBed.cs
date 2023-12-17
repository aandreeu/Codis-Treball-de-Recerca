using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepInBed : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    public bool CanSleep;
    [SerializeField] GameObject Player;
    [SerializeField] Animator FadeToBlack;

    void Start()
    {
        IsPlayerInRange = false;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (IsPlayerInRange)
        {
            if (Input.GetKey("e") && CanSleep)
            {
                FadeToBlack.SetTrigger("FadeOut");
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


}

