using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHealth;
    public float ActualHealth;
    public bool IsEnemyAlive;
    public bool isCovering;
    public Transform SpawnTrans;

    private void Start()
    {
        if (SpawnTrans == null)
        {
            SpawnTrans= gameObject.GetComponent<Transform>();
        }
        ActualHealth = MaxHealth;
        IsEnemyAlive = true;
    }

    public void GetDamage(float HitDamage)
    {
        if(IsEnemyAlive)
        {
            if (isCovering)
            {
                ActualHealth -= HitDamage * 0.15f;
                StartCoroutine(ChangeColor());
                //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100f, 0f));
            }
            else
            {
                StartCoroutine(ChangeColor());
                ActualHealth -= HitDamage;
            }
        }
    }
    private IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void PlayerDied()
    {
        transform.position = SpawnTrans.position;
        ActualHealth = MaxHealth;
    }

}

[System.Serializable]
public class EnemyMeleeAttack
{
    public float AttackRadius;
    public float AttackDamage;
    public GameObject AttackController;

    public EnemyMeleeAttack(float _attackRadius, float _attackDamage, GameObject _attackController)
    {
        this.AttackRadius = _attackRadius;
        this.AttackDamage = _attackDamage;
        this.AttackController = _attackController;
    }
}