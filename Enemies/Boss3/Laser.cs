using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float DealingDamage;
    [SerializeField] bool CanDealDamage;
    [SerializeField] PlayerController characterController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        characterController=FindObjectOfType<PlayerController>();
        if (collision.CompareTag("Player") && CanDealDamage)
        {
            characterController.PlayerGetDamage(DealingDamage);
            CanDealDamage=false;
        }
    }
}
