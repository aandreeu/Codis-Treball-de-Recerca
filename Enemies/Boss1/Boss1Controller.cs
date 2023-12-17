using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss1Controller : Enemy
{
    [SerializeField] bool IsActive;

    public EndMissionOnceDead endMissionOnceDead;
    public Animator animator;

    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[2];

    [Header("Cronometres")]
    [SerializeField] private bool CanChronoCount;
    [SerializeField] private float ChronoMeleeAttack;
    [SerializeField] private float ChronoDistanceAttack;
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
    [SerializeField] private float DistanceAttackRadium;
    [SerializeField] public Transform DistanceAttackPosition;
    [SerializeField] bool PlayerInDistanceAttackRange;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;

    private void Awake()
    {
        GeneralPlayer= GameObject.FindGameObjectWithTag("Player");
        animator=gameObject.GetComponent<Animator>();
        PlayerPos = GeneralPlayer.transform;
        endMissionOnceDead=gameObject.GetComponent<EndMissionOnceDead>();
    }
    void Start()
    {
        ActualHealth = MaxHealth;
        CanMove = true;
        CanAttack = true;
        IsEnemyAlive = false;
        IsActive = false;
    }

    private void Update()
    {
        if (ActualHealth <= 0 && IsEnemyAlive)
        {
            OnDie();
        }

        IsDetecting = Physics2D.OverlapCircle(transform.position, DetectingRadius, PlayerMask); //Definir cercles de deteccio
        PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, enemyMeleeAttacks[0].AttackRadius, PlayerMask);
        PlayerInDistanceAttackRange = Physics2D.OverlapCircle(DistanceAttackPosition.position, DistanceAttackRadium, PlayerMask);
        if (IsActive)
        {
            if (IsDetecting && !PlayerInAttackRange && CanMove) //Si s'ha de moure o no
            {
                animator.SetBool("Caminar", true);
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
                animator.SetBool("Caminar", false);
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
        IsEnemyAlive= false;
        endMissionOnceDead.EndOrAdvanceMission();
        GeneralPlayer.GetComponent<PlayerController>().AmagarEspasa();
        CanMove = false;
        CanAttack = false;
        animator.Play("Mort_Boss1");
    }

    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[2].AttackController.transform.position, enemyMeleeAttacks[2].AttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, DetectingRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(DistanceAttackPosition.position, DistanceAttackRadium);
    }

    void AttackChooser() //Triar aleatoriament l atac melee
    {
        switch (AttackDefiner=Random.Range(0,3))
        {
            case 0:
                animator.SetTrigger("Atac1");
                break;
            case 1:
                animator.SetTrigger("AtacEstocada");
                break;
            case 2:
                animator.SetTrigger("AtacCos");
                break;
        }
        ChronoMeleeAttack = 0;
    }

    public void ActivateDamageAttack1() //Fa el mal, cridat per l animator
    {
        Collider2D[] collided = Physics2D.OverlapCircleAll(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        foreach (Collider2D collided2 in collided)
        {
            if (collided2.CompareTag("Player"))
            {
                collided2.GetComponent<PlayerController>().PlayerGetDamage(enemyMeleeAttacks[0].AttackDamage);
            }
        }
    }
    public void ActivateDamageAttack2() //Fa el mal, cridat per l animator
    {
        Collider2D[] collided = Physics2D.OverlapCircleAll(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        foreach (Collider2D collided2 in collided)
        {
            if (collided2.CompareTag("Player"))
            {
                collided2.GetComponent<PlayerController>().PlayerGetDamage(enemyMeleeAttacks[1].AttackDamage);
            }
        }

    }
    public void ActivateDamageAttack3() //Fa el mal, cridat per l animator
    {
        Collider2D[] collided = Physics2D.OverlapCircleAll(enemyMeleeAttacks[2].AttackController.transform.position, enemyMeleeAttacks[2].AttackRadius);
        foreach (Collider2D collided2 in collided)
        {
            if (collided2.CompareTag("Player"))
            {
                collided2.GetComponent<PlayerController>().PlayerGetDamage(enemyMeleeAttacks[2].AttackDamage);
            }
        }

    }

    public void StopChrono(){
        CanChronoCount = false;
    }
    public void ResumeChrono() {
        CanChronoCount= true;
    }

    public void Freeze(){
        CanMove= false;
        CanAttack = false;
    }
    public void Unfreeze(){
        CanMove = true;
        CanAttack = true;
    }

    public void ActivateBoss()
    {
        //Animacio d engegar i fer IsActive per AnimationEvent
        IsActive= true;
        IsEnemyAlive = true;
        animator.Play("Idle_Boss1");
    }
}
