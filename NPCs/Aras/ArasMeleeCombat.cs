using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArasMeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform HitController;

    [SerializeField] private float HitRadius;
    [SerializeField] private float HitDamage;
    [SerializeField] private float BasicHitDamage;

    [SerializeField] private Animator anim;
    [SerializeField] ArasController playerController;

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            anim.SetTrigger("Atacar");
        }
    }
    public void MeleeAttack() //Cridat a traves de lanimator
    {
        Collider2D[] ObjectsInRange = Physics2D.OverlapCircleAll(HitController.position, HitRadius);
        foreach (Collider2D collided in ObjectsInRange)
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

}
