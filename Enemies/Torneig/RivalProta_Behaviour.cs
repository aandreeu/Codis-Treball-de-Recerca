using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalProta_Behaviour : Enemy
{
    [SerializeField] bool IsActive;

    public EndMissionOnceDead endMissionOnceDead;
    public Animator animator_rival;

    [Header("Cronometres")]
    [SerializeField] bool CanChronoCount;
    [SerializeField] float ChronoMeleeAttack;
    [SerializeField] float ChronoCarrerilla;
    [Header("Moviment")]
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] BoxCollider2D RbCollider;
    [SerializeField] bool IsFacingRight;
    [SerializeField] bool IsDashing;
    [SerializeField] bool IsAtacSalt;
    [SerializeField] bool HaXocat;
    [SerializeField] bool HasDealtDamage;
    [SerializeField] float DashPower;
    [SerializeField] float JumpAttackYForce;
    [SerializeField] float JumpAttackYForceDown;
    [SerializeField] bool IsDetecting;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float MovementSpeed;
    [Header("Atacs")]
    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[1];
    public int AttackDefiner;
    [SerializeField] int transicioDashSalt;
    [SerializeField] bool IsAttacking;
    [SerializeField] bool CanAttack;
    [SerializeField] bool PlayerInAttackRange;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;

    private void Awake()
    {
        GeneralPlayer = GameObject.FindGameObjectWithTag("Player");
        animator_rival = gameObject.GetComponent<Animator>();
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
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        RbCollider.enabled = false;
        animator_rival.Play("IdleAmbCama_RivalProta");
    }

    void Update()
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
                animator_rival.SetBool("Caminar", true);
                if (transform.position.x > PlayerPos.transform.position.x)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    IsFacingRight = false;
                }
                else if (transform.position.x < PlayerPos.transform.position.x)
                {
                    transform.localRotation = Quaternion.Euler(0, -180, 0);
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    IsFacingRight = true;
                }

                if (CanChronoCount)
                {
                    ChronoCarrerilla += Time.deltaTime;
                }
                if (ChronoCarrerilla >= 4)
                {
                    CanMove = false;
                    animator_rival.SetBool("Caminar", false);
                    animator_rival.SetTrigger("AtacDash");
                    ChronoCarrerilla = 0;
                }
            }
            else if (IsDetecting && PlayerInAttackRange && CanAttack) //Si esta al rang d'atac
            {
                animator_rival.SetBool("Caminar", false);
                if (CanChronoCount)
                {
                    ChronoMeleeAttack += Time.deltaTime;
                }
                if (ChronoMeleeAttack >= 0.75f)
                {
                    AttackChooser();
                    ChronoMeleeAttack = 0;
                }
            }
        }
    }

    public void AtacMelee1() //Fa el mal, cridat per l animator
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
    public void AtacMelee2() //Fa el mal, cridat per l animator
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
    private void OnDie() //Morir i donar per acabada la missio
    {
        IsEnemyAlive = false;
        endMissionOnceDead.EndOrAdvanceMission();
        CanMove = false;
        CanAttack = false;
        animator_rival.Play("Mort_RivalProta");
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
        switch (AttackDefiner = Random.Range(0, 3))
        {
            case 0:
                Freeze();
                animator_rival.SetTrigger("AtacPorra");
                break;
            case 1:
                Freeze();
                animator_rival.SetTrigger("AtacDavantDarrere");
                break;
            case 2:
                Freeze();
                animator_rival.SetTrigger("AtacSalt");
                break;
        }
        ChronoMeleeAttack = 0;
    }
    public void DashHoritzontal()
    {
        IsDashing = true;
        RbCollider.enabled = true;
        Physics2D.IgnoreCollision(RbCollider, GeneralPlayer.GetComponent<BoxCollider2D>());
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        if (IsFacingRight)
        {
            _rigidbody2D.AddForce(new Vector2(DashPower, 0), ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody2D.AddForce(new Vector2(-DashPower, 0), ForceMode2D.Impulse);
        }
    }
    public void TransicioSaltDash()
    {
        transicioDashSalt = Random.Range(0, 3);
        if (transicioDashSalt == 0)
        {
            animator_rival.SetBool("AtacDashPostSalt", true);
        }
        else
        {
            animator_rival.SetBool("AtacDashPostSalt", false);
        }
    }
    public void AtacSaltDash()
    {
        Physics2D.IgnoreCollision(RbCollider, GeneralPlayer.GetComponent<BoxCollider2D>());
        IsAtacSalt = true;
        RbCollider.enabled = true;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.AddForce(new Vector2(0, JumpAttackYForce), ForceMode2D.Impulse);
    }
    public void AtacSaltDashCapAvall()
    {
        Physics2D.IgnoreCollision(RbCollider, GeneralPlayer.GetComponent<BoxCollider2D>());
        IsAtacSalt = true;
        RbCollider.enabled = true;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.AddForce(new Vector2(0, -JumpAttackYForceDown), ForceMode2D.Impulse);
        
    }
    public void ApagarRb()
    {
        RbCollider.enabled = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        IsAtacSalt = false;
        IsDashing= false;
        transicioDashSalt=3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDashing && collision.gameObject.CompareTag("Player") && !HasDealtDamage)
        {
            collision.GetComponent<PlayerController>().PlayerGetDamage(enemyMeleeAttacks[1].AttackDamage);
            HasDealtDamage = true;
        }
    }

    public void ActivarBoss()
    {
        IsActive= true;
        animator_rival.Play("TreureCama_RivalProta");
    }

    public void Freeze()
    {
        CanMove = false;
        CanChronoCount = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    public void Unfeeze()
    {
        CanMove = true;
        CanChronoCount = true;
    }
}
