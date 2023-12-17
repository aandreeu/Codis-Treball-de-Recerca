using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] GameObject Player;
    [SerializeField] Animator LiftAnimator;

    void Start()
    {
        IsPlayerInRange = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        LiftAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (IsPlayerInRange)
        {
            if (Input.GetKey("e"))
            {
                LiftAnimator.Play("Ascensor_Baixa");
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
