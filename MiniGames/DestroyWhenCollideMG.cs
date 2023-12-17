using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenCollideMG : MonoBehaviour
{
    [SerializeField] PlayerController characterController;
    [SerializeField] float Damage;
    public bool CanDealDamage;

    private void Start()
    {
        characterController=FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && CanDealDamage)
        {
            characterController.PlayerGetDamage(Damage);
            Destroy(gameObject);
        }
    }
    public void SetCanDealDamage(bool active)
    {
        CanDealDamage= active;
    }
}
