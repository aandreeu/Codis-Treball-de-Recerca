using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArasBossController : Enemy
{
    [SerializeField] bool IsActive;

    public EndMissionOnceDead endMissionOnceDead;
    public Animator ArasBossAnimator;
    [SerializeField] BoxCollider2D colliderPressE;
    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[2];

    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D RBcollider;
    [SerializeField] BoxCollider2D Triggercollider;
    [Header("Cronometres")]
    [SerializeField] private bool CanChronoCount;
    [SerializeField] private float ChronoMeleeAttack;
    [Header("Moviment")]
    [SerializeField] bool IsDetecting;
    [SerializeField] bool IsFacingRight;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float MovementSpeed;
    [Header("Atacs")]
    [SerializeField] bool IsAttacking;
    [SerializeField] bool IsDashing;
    [SerializeField] bool CanAttack;
    [SerializeField] bool PlayerInAttackRange;
    [SerializeField] int AttackDefiner;
    [SerializeField] float DangerRadius;
    [SerializeField] float DashPower;
    [SerializeField] bool PlayerInCoverRange;
    [SerializeField] float CoverRadius;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;

    private void Awake()
    {
        GeneralPlayer = GameObject.FindGameObjectWithTag("Player");
        ArasBossAnimator = gameObject.GetComponent<Animator>();
        PlayerPos = GeneralPlayer.transform;
        endMissionOnceDead = gameObject.GetComponent<EndMissionOnceDead>();
    }
    void Start()
    {
        ActualHealth = MaxHealth;
        CanMove = true;
        CanAttack = true;
        IsEnemyAlive = true;
        IsActive = false;
        Triggercollider.isTrigger= true;
        Physics2D.IgnoreCollision(RBcollider, GeneralPlayer.GetComponent<BoxCollider2D>());
    }

    private void Update()
    {
        if (ActualHealth <= 0 && IsEnemyAlive)
        {
            OnDie();
        }

        IsDetecting = Physics2D.OverlapCircle(transform.position, DetectingRadius, PlayerMask); //Definir cercles de deteccio
        PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, DangerRadius, PlayerMask);
        PlayerInCoverRange = Physics2D.OverlapCircle(transform.position, CoverRadius, PlayerMask);
        if (IsActive)
        {
            if (IsDetecting && !PlayerInCoverRange && !PlayerInAttackRange && CanMove) //Moures sense cobrirse
            {
                isCovering = false;
                ArasBossAnimator.SetBool("Caminar", true);
                ArasBossAnimator.SetBool("IsArasCovering", false);
                if (transform.position.x > PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    IsFacingRight = false;
                }
                else if (transform.position.x < PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, -180, 0);
                    IsFacingRight = true;
                }
            }
            else if (IsDetecting && PlayerInCoverRange && !PlayerInAttackRange && CanMove && !IsAttacking) //Si s'ha de moure cobrintse
            {
                ArasBossAnimator.SetBool("Caminar", true);
                ArasBossAnimator.SetBool("IsArasCovering", true);
                isCovering = true;
                if (transform.position.x > PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    IsFacingRight = false;

                }
                else if (transform.position.x < PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, -180, 0);
                    IsFacingRight = true;
                }
            }
            else if (IsDetecting && PlayerInCoverRange&& PlayerInAttackRange && CanAttack) //Si esta al rang d'atac
            {
                ArasBossAnimator.SetBool("Caminar", false);
                if (!IsAttacking)
                {
                    ArasBossAnimator.SetBool("IsArasCovering", true);
                    isCovering = true;
                }

                if (CanChronoCount)
                {
                    ChronoMeleeAttack += Time.deltaTime;
                }
                if (ChronoMeleeAttack >= 0.5)
                {
                    AttackChooser();
                }
            }
        }
    }

    private void OnDie() //Morir i donar per acabada la missio
    {
        IsEnemyAlive = false;
        IsActive= false;
        CanMove = false;
        CanAttack = false;
        CanChronoCount= false;
        colliderPressE.enabled = true;
        endMissionOnceDead.EndOrAdvanceMission();
    }

    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[2].AttackController.transform.position, enemyMeleeAttacks[2].AttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, DetectingRadius);
        Gizmos.DrawWireSphere(gameObject.transform.position, DangerRadius);
        Gizmos.DrawWireSphere(gameObject.transform.position, CoverRadius);
    }

    void AttackChooser() //Triar aleatoriament l atac melee
    {
        IsAttacking = true; 
        isCovering = false;
        switch (AttackDefiner = Random.Range(0, 5))
        {
            case 0:
                ArasBossAnimator.Play("CopDePuny1Unic_Aras");
                break;
            case 1:
                ArasBossAnimator.Play("PatadaAerea_Aras");
                break;
            case 2:
                ArasBossAnimator.Play("CopDePunyAcotat_Aras");
                break;
            case 3:
                ArasBossAnimator.Play("CopDePuny1_Aras");
                break;
            case 4:
                ArasBossAnimator.Play("Tall_Aras");
                break;
        }
        ChronoMeleeAttack = 0;
    }

    public void SlightDash(float power)
    {
        DashPower = power;
        RBcollider.enabled = true;
        IsDashing = true;
        Physics2D.IgnoreCollision(RBcollider, GeneralPlayer.GetComponent<BoxCollider2D>());
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        if (IsFacingRight)
        {
            rb2d.AddForce(new Vector2(DashPower, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(new Vector2(-DashPower, 0), ForceMode2D.Impulse);
        }
    }
    public void ApagarRb()
    {
        Physics2D.IgnoreCollision(RBcollider, GeneralPlayer.GetComponent<BoxCollider2D>());
        rb2d.bodyType = RigidbodyType2D.Static;
        IsDashing = false;
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
    public void StopChrono()
    {
        CanChronoCount = false;
    }
    public void ResumeChrono()
    {
        CanChronoCount = true;
    }

    public void FreezeBoss()
    {
        CanMove = false;
        CanAttack = false;
        CanChronoCount = false;
    }
    public void UnfreezeBoss()
    {
        CanMove = true;
        CanAttack = true;
        CanChronoCount = true;
    }

    public void StopAttacking()
    {
        IsAttacking= false;
        CanAttack = true;
    }
    public void ActivateBoss()
    {
        //Animacio d engegar i fer IsActive per AnimationEvent
        IsActive = true;

    }
}
