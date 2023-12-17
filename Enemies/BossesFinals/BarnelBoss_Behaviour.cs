using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BarnelBoss_Behaviour : Enemy
{
    public bool IsActive;

    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[1];
    public GameObject Barnel_Gnl;
    [SerializeField] Animator BarnelAnimator;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D colliderRB;
    [SerializeField] BoxCollider2D triggerCollider;
    [SerializeField] BoxCollider2D DialogueCollider;

    [Header("Cronometres")]
    [SerializeField] private bool CanChronoCount;
    [SerializeField] private bool CanChronoAtacFinalCount;
    [SerializeField] private float ChronoMeleeAttack;
    [SerializeField] private float ChronoAtacFinal;
    [Header("Moviment")]
    public bool FacingRight;
    [SerializeField] bool IsDetecting;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float MovementSpeed;
    [Header("Atacs")]
    [SerializeField] bool IsAttacking;
    [SerializeField] bool IsDodging;
    [SerializeField] bool IsAtacFinal;
    [SerializeField] bool CanAttack;
    [SerializeField] bool PlayerInAttackRange;
    [SerializeField] int AttackDefiner;
    [SerializeField] float TotalAttackRadius;
    [SerializeField] float JumpAttackXForce;
    [SerializeField] float DodgeForce;
    [SerializeField] float JumpAttackYForce;
    [SerializeField] bool CorrerFinsAtacFinal;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;
    [SerializeField] EndMissionOnceDead endMissionOnceDead;
    [SerializeField] BarnelBulletSpawner barnelBulletSpawner;

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
        IsAtacFinal = false;
        colliderRB.enabled = false;
        triggerCollider.enabled = true;
        DialogueCollider.enabled = true;
        gameObject.tag = "Enemy";
        rb2d.bodyType = RigidbodyType2D.Static;
        CanChronoCount = true;
        CanChronoAtacFinalCount = true;
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
            if (CorrerFinsAtacFinal && CanMove) //Anar a la paret dreta
            {
                BarnelAnimator.SetBool("Correr", true);
                transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                return;
            }

            if (IsDetecting && !PlayerInAttackRange && CanMove && !IsDodging) //Si s ha de moure o no, cap al jugador
            {
                BarnelAnimator.SetBool("Correr", true);
                if (transform.position.x > PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    FacingRight = false;
                }
                else
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    FacingRight = true;
                }

                if (ChronoAtacFinal >= 7f)
                {
                    IniciarAtacFinal();
                }
                if (CanChronoAtacFinalCount)
                {
                    ChronoAtacFinal += Time.deltaTime;
                }
            }
            else if (IsDetecting && PlayerInAttackRange && CanAttack) //Si esta al rang d'atac
            {
                BarnelAnimator.SetBool("Correr", false);
                if (Input.GetKeyDown("z") && !IsDodging)
                {
                    BarnelAnimator.SetTrigger("Esquivar");
                    StopChrono();
                    IsDodging = true;
                }

                if (CanChronoCount)
                {
                    ChronoMeleeAttack += Time.deltaTime;
                }
                if (ChronoMeleeAttack >= 0.8f)
                {
                    BarnelAnimator.SetTrigger("AtacMelee");
                    ChronoMeleeAttack = 0f;
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
        BarnelAnimator.Play("Mort_Barnel");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PosicioAtacFinal") && CorrerFinsAtacFinal)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            BarnelAnimator.SetBool("Correr", false);
            BarnelAnimator.SetTrigger("AtacFinal");
            barnelBulletSpawner.BalaActual = 0;
            barnelBulletSpawner.disparaRafaga = true;
            BarnelAnimator.SetBool("SeguirDisparant", true);
            ChronoAtacFinal = 0f;
            CanChronoCount = false;
            IsAtacFinal = true;
            CorrerFinsAtacFinal = false;
        }
    }

    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, TotalAttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, DetectingRadius);
    }

    public void IniciarAtacFinal()
    {
        CorrerFinsAtacFinal = true;
        IsAtacFinal = false;
    }
    public void DashAtacMelee()
    {
        colliderRB.enabled = true;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        if (FacingRight)
        {
            rb2d.AddForce(new Vector2(JumpAttackXForce, JumpAttackYForce), ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(new Vector2(-JumpAttackXForce, JumpAttackYForce), ForceMode2D.Impulse);
        }
    }
    public void DashEsquivar()
    {
        colliderRB.enabled = true;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        if (FacingRight)
        {
            rb2d.AddForce(new Vector2(-DodgeForce, JumpAttackYForce), ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(new Vector2(DodgeForce, JumpAttackYForce), ForceMode2D.Impulse);
        }
    }
    public void AcabarDodge()
    {
        IsDodging = false;
        int Contraatacar = Random.Range(0, 4);
        Debug.Log(Contraatacar);
        if(Contraatacar > 0)
        {
            BarnelAnimator.SetBool("DispararPostEsquivar", true);
            Freeze();
        }
    }
    public void DispararEscopetadaDodge()
    {
        BarnelAnimator.SetBool("DispararPostEsquivar", false);
        barnelBulletSpawner.SpawnEscopetada();
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
    public void StopChrono()
    {
        CanChronoCount = false;
        CanChronoAtacFinalCount = false;
    }
    public void ResumeChrono()
    {
        CanChronoCount = true;
        CanChronoAtacFinalCount = true;
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
    public void ApagarRb()
    {
        colliderRB.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    public void ActivateBoss()
    {
        //Animacio d engegar i fer IsActive per AnimationEvent
        IsActive = true;

    }
}
