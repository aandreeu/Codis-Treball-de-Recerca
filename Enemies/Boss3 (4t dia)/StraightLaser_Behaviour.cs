using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLaser_Behaviour : MonoBehaviour
{
    [SerializeField] float LaserDamage;

    private void OnEnable()
    {
        //animator.Play("LoadLaser");
        Invoke("LaserDealDamage", 2f);
    }
    public void LaserDealDamage() //Cridada des de l animator quan el tingui
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Invoke("ApagarLaser", 0.2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.PlayerGetDamage(LaserDamage);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void ApagarLaser()
    {
        gameObject.SetActive(false);
    }
    // Per l animator l apaguem
}
