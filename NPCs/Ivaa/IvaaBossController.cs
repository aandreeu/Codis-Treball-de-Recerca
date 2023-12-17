using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IvaaBossController : Enemy
{
    public bool IsActive;

    public EnemyMeleeAttack[] enemyMeleeAttacks = new EnemyMeleeAttack[2];
    public GameObject Ivaaa_Gnl;
    [SerializeField] Animator IvaaAnimator;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D colliderRB;

    [Header("Cronometres")]
    [SerializeField] private bool CanChronoCount;
    [SerializeField] private bool CanChronoAtacVolatCount;
    [SerializeField] private float ChronoMeleeAttack;
    [SerializeField] private float ChronoAtacVolat;
    [Header("Moviment")]
    public bool FacingRight;
    [SerializeField] bool IsDetecting;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float MovementSpeed;
    [Header("Atacs")]
    //[SerializeField] BulletSpwner_4Boss bulletSpawner;
    [SerializeField] bool IsAttacking;
    [SerializeField] bool IsAtacVolat;
    [SerializeField] bool IsAtacSalt;
    [SerializeField] bool CaminarFinsVell;
    [SerializeField] bool CanAttack;
    [SerializeField] bool PlayerInAttackRange;
    [SerializeField] int AttackDefiner;
    [SerializeField] PlayableDirector AtacVolant_sequence;
    [SerializeField] float JumpAttackRadius;
    [SerializeField] float JumpAttackXForce;
    [SerializeField] float JumpAttackYForce;
    [SerializeField] bool CaminarFinsJugador;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public GameObject GeneralPlayer;
    public IvaaBulletSpawner ivaaBulletSpawner;
    [SerializeField] EndMissionOnceDead endMissionOnceDead;

    private void Awake()
    {
        GeneralPlayer = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = GeneralPlayer.transform;
    }
    void Start()
    {
        //bulletSpawner.enabled = false;
        ActualHealth = MaxHealth;
        CanMove = true;
        CanAttack = true;
        IsEnemyAlive = true;
        IsActive = true;
        IsAtacVolat = false;
        CanChronoAtacVolatCount = true;
        colliderRB.enabled=false;
        gameObject.tag = "Enemy";
        rb2d.bodyType = RigidbodyType2D.Static;
        CanChronoCount = true;
        this.enabled = false;
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
            if(CaminarFinsVell && !IsAtacVolat && CanMove)
            {
                IvaaAnimator.SetBool("Caminar", true);
                transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                return;
            }

            if(CaminarFinsJugador && !IsAtacSalt && !IsAtacVolat && CanMove)
            {
                bool RangDeSalt = Physics2D.OverlapCircle(transform.position, JumpAttackRadius, PlayerMask);
                if (RangDeSalt)
                {
                    CaminarFinsJugador = false;
                    IsAtacSalt= true;
                    IvaaAnimator.SetTrigger("SaltDispar");
                }
                else
                {
                    IvaaAnimator.SetBool("Caminar", true);
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
                    return;
                }
            }

            if (IsDetecting && !PlayerInAttackRange && CanMove && !IsAtacVolat && !IsAtacSalt) //Si s'ha de moure o no
            {
                IvaaAnimator.SetBool("Caminar", true);
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

                if (ChronoAtacVolat >= 6f)
                {
                    IniciarAtacVolat();
                }
                if (CanChronoAtacVolatCount)
                {
                    ChronoAtacVolat += Time.deltaTime;
                }


            }
            else if (IsDetecting && PlayerInAttackRange && CanAttack) //Si esta al rang d'atac
            {
                IvaaAnimator.SetBool("Caminar", false);
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
        IvaaAnimator.Play("Mort_Ivaa");
    }

    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyMeleeAttacks[0].AttackController.transform.position, enemyMeleeAttacks[0].AttackRadius);
        Gizmos.DrawWireSphere(enemyMeleeAttacks[1].AttackController.transform.position, enemyMeleeAttacks[1].AttackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, JumpAttackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, DetectingRadius);
    }

    void AttackChooser() //Triar aleatoriament l atac melee
    {
        switch (AttackDefiner = Random.Range(0, 2))
        {
            case 1:
                Freeze();
                IvaaAnimator.SetTrigger("CopDePuny");
                break;
            case 2:
                Freeze();
                IvaaAnimator.SetTrigger("Patada");
                break;
        }
        ChronoMeleeAttack = 0;
    }
    public void IniciarAtacVolat()
    {
        CanChronoAtacVolatCount = false;
        CaminarFinsVell = true;
        IsAtacVolat = false;
    }
    public void SaltDispar()
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
    public void DispararEnSaltDispar(int i)
    {
        ivaaBulletSpawner.SpawnBulletSaltDispar(i);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("VellTedd") && CaminarFinsVell)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            IsAtacVolat=true;
            IvaaAnimator.SetBool("Caminar", false);
            ChronoAtacVolat = 0f;
            CanChronoCount= false;
            CaminarFinsVell = false;
            AtacVolant_sequence.Play();
            Debug.Log("Animacio  " + collision.name);
        }
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
        IsAtacVolat = false;
    }
    public void ApagarRb()
    {
        colliderRB.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        IsAtacSalt= false;
    }

    public void ActivateBoss()
    {
        //Animacio d engegar i fer IsActive per AnimationEvent
        IsActive = true;

    }
}
