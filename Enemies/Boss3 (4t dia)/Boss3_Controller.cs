using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3_Controller : Enemy
{
    [SerializeField] bool IsActive;

    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[2];
    public GameObject Boss3_Gnl;
    [SerializeField] Animator Boss3Animator;
    [SerializeField] GameObject LoopSangDavant;
    [SerializeField] GameObject LoopSangDarrere;
    [Header("Cronometres")]
    [SerializeField] private bool CanChronoCount;
    [SerializeField] private float ChronoMeleeAttack;
    [Header("Moviment")]
    [SerializeField] bool IsDetecting;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float MovementSpeed;
    [Header("Atacs")]
    [SerializeField] bool BulletsSecondTime;
    [SerializeField] bool BulletsFirstTime;
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] bool IsAttacking;
    [SerializeField] bool CanAttack;
    [SerializeField] bool PlayerInAttackRange;
    [SerializeField] int AttackDefiner;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;
    [SerializeField] EndMissionOnceDead endMissionOnceDead;

    private void Awake()
    {
        GeneralPlayer = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = GeneralPlayer.transform;
    }
    void Start()
    {
        bulletSpawner.enabled= false;
        ActualHealth = MaxHealth;
        BulletsSecondTime = false;
        CanMove = true;
        CanAttack = true;
        IsEnemyAlive = true;
        IsActive = true;
        CanChronoCount = true;
        LoopSangDarrere.SetActive(false);
        LoopSangDavant.SetActive(false);

    }

    private void Update()
    {
        if (ActualHealth <= 0 && IsEnemyAlive)
        {
            OnDie();
        }
        if (ActualHealth <= MaxHealth/2 && !BulletsFirstTime && IsEnemyAlive)
        {
            BulletsFirstTime = true;
            Boss3Animator.SetBool("Caminar", false);
            Boss3Animator.ResetTrigger("Atacar1");
            Boss3Animator.ResetTrigger("Colpejar");
            Boss3Animator.SetTrigger("Atacar2");
        }
        if (ActualHealth <= MaxHealth/4 && BulletsFirstTime && !BulletsSecondTime && IsEnemyAlive)
        {
            BulletsSecondTime = true;
            Boss3Animator.SetBool("Caminar", false);
            Boss3Animator.ResetTrigger("Atacar1");
            Boss3Animator.ResetTrigger("Colpejar");
            Boss3Animator.SetTrigger("Atacar2");
        }

        IsDetecting = Physics2D.OverlapCircle(transform.position, DetectingRadius, PlayerMask); //Definir cercles de deteccio
        PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, enemyMeleeAttacks[0].AttackRadius, PlayerMask);
        if (IsActive)
        {
            if (IsDetecting && !PlayerInAttackRange && CanMove) //Si s'ha de moure o no
            {
                Boss3Animator.SetBool("Caminar", true);
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
                Boss3Animator.SetBool("Caminar", false);
                if (CanChronoCount)
                {
                    ChronoMeleeAttack += Time.deltaTime;
                }
                if (ChronoMeleeAttack >= 0.5f)
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
        endMissionOnceDead.EndOrAdvanceMission();
        Boss3Animator.Play("Mort_Kicki");
    }

    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[2].AttackController.transform.position, enemyMeleeAttacks[2].AttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, DetectingRadius);
    }

    void AttackChooser() //Triar aleatoriament l atac melee
    {
        switch (AttackDefiner = Random.Range(0, 3))
        {
            case 1:
                Freeze();
                Boss3Animator.SetTrigger("Colpejar");
                break;
            case 2:
                Freeze();
                Boss3Animator.SetTrigger("Atacar1");
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
                Debug.Log("MalFet");
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
                Debug.Log("MalFet");

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
                Debug.Log("MalFet");
            }
        }
    }
    public void ActivarLoopsSang()
    {
        LoopSangDarrere.SetActive(true);
        LoopSangDavant.SetActive(true);
        LoopSangDarrere.GetComponent<Animator>().Play("LoopSangDarrere_Kicki");
        LoopSangDavant.GetComponent<Animator>().Play("LoopSangDavant_Kicki");
        bulletSpawner.enabled = true;
        Boss3Animator.SetBool("SeguirDisparant", true);
    }
    public void ApagarLoopsSang()
    {
        LoopSangDarrere.GetComponent<Animator>().SetTrigger("FadeOut");
        LoopSangDavant.GetComponent<Animator>().SetTrigger("FadeOut");
        Invoke("Unfreeze", 2.5f);
        Invoke("ResumeChrono", 2.5f);
    }
    public void SpawnBola()
    {
        bulletSpawner.SpawnBullet();
    }

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
