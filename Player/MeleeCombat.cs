using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform HitController;

    [SerializeField] private float HitRadius;
    [SerializeField] private float HitDamage;
    [SerializeField] private float BasicHitDamage;

    [SerializeField] private Animator anim;
    [SerializeField] PlayerController playerController;

    void Update()
    {
        if (Input.GetKeyDown("z") && !playerController.CanPressCounter)
        {
            anim.SetTrigger("Atacar");
        }
        if (Input.GetKeyDown("x") && playerController.IsSwordOut)
        {
            anim.SetTrigger("Escopetada");
        }
    }
    public void MeleeAttack() //Cridat a traves de lanimator
    {
        Collider2D[] ObjectsInRange = Physics2D.OverlapCircleAll(HitController.position, HitRadius);
        foreach(Collider2D collided in ObjectsInRange)
        {
            if (collided.CompareTag("Enemy"))
            {
                collided.gameObject.TryGetComponent<Enemy>(out Enemy enemyComp);
                enemyComp.GetDamage(HitDamage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitController.position, HitRadius);
    }

    public void BuffDamageAttack(float multiplier){
        HitDamage= (1+multiplier)*HitDamage;    
    }
    public void DebuffDamageAttack(float multiplier)
    {
        HitDamage =  HitDamage / (1 + multiplier); 
    }
}
