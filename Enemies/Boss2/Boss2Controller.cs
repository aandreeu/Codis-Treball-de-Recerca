using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2Controller : Enemy
{
    [SerializeField] bool IsActive;

    public EndMissionOnceDead endMissionOnceDead;
    public Animator animator_bou;

    [Header("Cronometres")]
    [SerializeField] bool CanChronoCount;
    [SerializeField] float ChronoMeleeAttack;
    [SerializeField] float ChronoCarrerilla;
    [Header("Moviment")]
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] BoxCollider2D RbCollider;
    [SerializeField] bool IsFacingRight;
    [SerializeField] bool IsBouFliping;
    [SerializeField] bool IsPlayerRight;
    [SerializeField] bool IsDashing;
    [SerializeField] bool HaXocat;
    [SerializeField] bool HasDealtDamage;
    [SerializeField] float CarrerillaTime;
    [SerializeField] float CarrerillaPower;
    [SerializeField] bool IsDetecting;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float MovementSpeed;
    [Header("Atacs")]
    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[1];
    [SerializeField] bool IsAttacking;
    [SerializeField] bool CanAttack;
    [SerializeField] bool PlayerInAttackRange;
    [SerializeField] int DistanceToRun;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;

    private void Awake()
    {
        GeneralPlayer = GameObject.FindGameObjectWithTag("Player");
        animator_bou = gameObject.GetComponent<Animator>();
        PlayerPos = GeneralPlayer.transform;
        endMissionOnceDead = gameObject.GetComponent<EndMissionOnceDead>();
    }

    void Start()
    {
        ActualHealth = MaxHealth;
        CanMove = true;
        CanAttack = true;
        IsEnemyAlive = false;
        IsActive = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        RbCollider.enabled = false;

    }

    void Update()
    {
        if (ActualHealth <= 0 && IsEnemyAlive)
        {
            OnDie();
        }

        if (transform.position.x > PlayerPos.transform.position.x)
        {
            IsPlayerRight = false;
        }
        else
        {
            IsPlayerRight = true;
        }

        IsDetecting = Physics2D.OverlapCircle(transform.position, DetectingRadius, PlayerMask); //Definir cercles de deteccio
        PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, enemyMeleeAttacks[0].AttackRadius+0.5f, PlayerMask);
        if (IsActive)
        {
            if (IsDetecting && !PlayerInAttackRange && CanMove) //Si s'ha de moure o no
            {

                if(IsFacingRight && !IsPlayerRight && !IsBouFliping)
                {
                    animator_bou.SetBool("Caminar", false);
                    animator_bou.SetTrigger("Girar");
                    IsBouFliping = true;
                }
                else if(!IsFacingRight && IsPlayerRight && !IsBouFliping)
                {
                    animator_bou.SetBool("Caminar", false);
                    animator_bou.SetTrigger("Girar");
                    IsBouFliping = true;
                }

                if (transform.position.x > PlayerPos.transform.position.x && !IsBouFliping)
                {
                    animator_bou.SetBool("Caminar", true);
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                }
                else if(transform.position.x < PlayerPos.transform.position.x && !IsBouFliping)
                {
                    animator_bou.SetBool("Caminar", true);
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                }

                if (CanChronoCount)
                {
                    ChronoCarrerilla += Time.deltaTime;
                }
                if(ChronoCarrerilla>=6)
                {
                    CanMove= false;
                    animator_bou.SetBool("Caminar", false);
                    animator_bou.SetTrigger("PreparaCarrerilla");
                    ChronoCarrerilla= -2f;
                }
            }
            else if (IsDetecting && PlayerInAttackRange && CanAttack) //Si esta al rang d'atac
            {
                animator_bou.SetBool("Caminar", false);
                if (CanChronoCount)
                {
                    ChronoMeleeAttack += Time.deltaTime;
                }
                if (ChronoMeleeAttack >= 0.6f)
                {
                    animator_bou.SetTrigger("Atacar");
                    ChronoMeleeAttack= 0;
                }
            }
        }
    }

    public void GirarBou()
    {
        if (IsFacingRight){
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            IsFacingRight= false;
        }
        else{
            transform.localRotation = Quaternion.Euler(0,-180, 0);
            IsFacingRight = true;
        }
    }
    public void PararDeGirarBou()
    {
        IsBouFliping= false;
    }

    public void AtacCornada() //Fa el mal, cridat per l animator
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
    public void AtacPatada() //Fa el mal, cridat per l animator
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
        GeneralPlayer.GetComponent<PlayerController>().AmagarEspasa();
        CanAttack = false;
        animator_bou.Play("Mort_Bou");
    }
    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, DetectingRadius);
    }

    private IEnumerator Dash()
    {
        IsDashing = true;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        RbCollider.enabled = true;
        Physics2D.IgnoreCollision(RbCollider, GeneralPlayer.GetComponent<BoxCollider2D>());
        _rigidbody2D.gravityScale = 0f;
        if (IsFacingRight)
        {
            _rigidbody2D.velocity = new Vector2(transform.localScale.x * CarrerillaPower, 0f);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(-transform.localScale.x * CarrerillaPower, 0f);
        }
        yield return new WaitForSeconds(CarrerillaTime);
        _rigidbody2D.gravityScale = 2f;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        RbCollider.enabled=false;
        IsDashing = false;
        CanMove = true;
        if (!HaXocat)
        {
            animator_bou.SetTrigger("AtacaCarrerilla");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDashing && collision.gameObject.CompareTag("Paret") && !HaXocat)
        {
            Debug.Log(collision.name);
            HaXocat = true;
            animator_bou.SetTrigger("Xocar");
            IsDashing= false;
        }

        if (IsDashing && collision.gameObject.CompareTag("Player") && !HaXocat && !HasDealtDamage)
        {
            collision.GetComponent<PlayerController>().PlayerGetDamage(enemyMeleeAttacks[1].AttackDamage);
            HasDealtDamage=true;
        }
    }

    public void ActivateBoss()
    {
        //Animacio d engegar i fer IsActive per AnimationEvent
        IsActive = true;
        IsEnemyAlive = true;
        animator_bou.Play("Idle_Bou");
    }

    public void FreezeBou()
    {
        CanMove= false;
        CanChronoCount=false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    public void UnfeezeBou()
    {
        CanMove= true;
        CanChronoCount=true;
    }
    public void DeixarDeXocar()
    {
        HaXocat=false;
        HasDealtDamage=false;
    }
}
