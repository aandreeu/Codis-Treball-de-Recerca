using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] bool IsActive;

    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[2];

    [Header("Cronometres")]
    [SerializeField] private bool CanChronoCount;
    [SerializeField] private float ChronoMeleeAttack;
    [Header("Moviment")]
    [SerializeField] bool IsDetecting;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float MovementSpeed;
    [Header("Atacs")]
    [SerializeField] bool IsAttacking;
    [SerializeField] bool CanAttack;
    [SerializeField] bool PlayerInAttackRange;
    [SerializeField] int AttackDefiner;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;

    private void Awake()
    {
        GeneralPlayer = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = GeneralPlayer.transform;
    }
    void Start()
    {
        ActualHealth = MaxHealth;
        CanMove = true;
        CanAttack = true;
        IsEnemyAlive = true;
        IsActive = true;
        CanChronoCount= true;
    }

    private void Update()
    {
        if (ActualHealth <= 0 && IsEnemyAlive)
        {
            OnDie();
        }

        IsDetecting = Physics2D.OverlapCircle(transform.position, DetectingRadius, PlayerMask); //Definir cercles de deteccio
        PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, enemyMeleeAttacks[0].AttackRadius, PlayerMask);
        if (IsActive)
        {
            if (IsDetecting && !PlayerInAttackRange && CanMove) //Si s'ha de moure o no
            {
                if (transform.position.x > PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else if (IsDetecting && PlayerInAttackRange && CanAttack) //Si esta al rang d'atac
            {
                if (CanChronoCount)
                {
                    ChronoMeleeAttack += Time.deltaTime;
                }
                if (ChronoMeleeAttack >= 1)
                {
                    AttackChooser();
                }
            }
        }
    }

    private void OnDie() //Morir i donar per acabada la missio
    {
        IsEnemyAlive = false;
        CanMove = false;
        CanAttack = false;
        Destroy(gameObject);
    }

    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, DetectingRadius);
    }

    void AttackChooser() //Triar aleatoriament l atac melee
    {
        switch (AttackDefiner = Random.Range(0, enemyMeleeAttacks.Length + 1))
        {
            case 1:
                Debug.Log("Ataca1");
                break;
            case 2:
                Debug.Log("Ataca2");
                break;
        }
        ChronoMeleeAttack = 0;
    }

    //public void ActivateDamageAttack1() //Fa el mal, cridat per l animator
    //{
    //    Collider2D[] collided = Physics2D.OverlapCircleAll(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
    //    foreach (Collider2D collided2 in collided)
    //    {
    //        if (collided2.CompareTag("Player"))
    //        {
    //            collided2.GetComponent<PlayerController>().PlayerGetDamage(enemyMeleeAttacks[0].AttackDamage);
    //        }
    //    }
    //}
    //public void ActivateDamageAttack2() //Fa el mal, cridat per l animator
    //{
    //    Collider2D[] collided = Physics2D.OverlapCircleAll(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
    //    foreach (Collider2D collided2 in collided)
    //    {
    //        if (collided2.CompareTag("Player"))
    //        {
    //            collided2.GetComponent<PlayerController>().PlayerGetDamage(enemyMeleeAttacks[1].AttackDamage);
    //        }
    //    }

    //}

    public void StopChrono()
    {
        CanChronoCount = false;
    }
    public void ResumeChrono()
    {
        CanChronoCount = true;
    }

    public void Freeze()
    {
        CanMove = false;
        CanAttack = false;
    }
    public void Unfreeze()
    {
        CanMove = true;
        CanAttack = true;
    }

    public void ActivateBoss()
    {
        //Animacio d engegar i fer IsActive per AnimationEvent
        IsActive = true;

    }
}
