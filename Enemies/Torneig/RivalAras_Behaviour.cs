using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAras_Behaviour : Enemy
{
    [SerializeField] bool IsActive;

    public EndMissionOnceDead endMissionOnceDead;
    public Animator animator_rival;

    [Header("Moviment")]
    [SerializeField] bool IsStunned;
    [SerializeField] bool Vola;
    [SerializeField] bool IsFacingRight;
    [SerializeField] bool IsDetecting;
    [SerializeField] bool CanMove;
    [SerializeField] float DetectingRadius;
    [SerializeField] float AttackRange;
    [SerializeField] float MovementSpeed;
    [SerializeField] bool PlayerInAttackRange;
    [Header("Jugador")]
    [SerializeField] LayerMask PlayerMask;
    public Transform PlayerPos;
    public Transform transformdetect;
    public GameObject GeneralPlayer;

    private void Awake()
    {
        animator_rival = gameObject.GetComponent<Animator>();
        endMissionOnceDead = gameObject.GetComponent<EndMissionOnceDead>();
    }

    void Start()
    {
        ActualHealth = MaxHealth;
        CanMove = true;
        IsEnemyAlive = true;
        IsActive = true;
        IsStunned= false;
        Vola= false;
    }

    void Update()
    {
        IsDetecting = Physics2D.OverlapCircle(transformdetect.position, DetectingRadius, PlayerMask); //Definir cercles de deteccio
        PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, AttackRange, PlayerMask);

        if (ActualHealth < 30 && IsEnemyAlive)
        {
            animator_rival.SetTrigger("Stun");
            IsStunned = true;
        }
        if (ActualHealth <= 0 && IsEnemyAlive)
        {
            OnDie();
        }

        if(Vola)
        {
            transform.Translate(Vector3.right * 25f * Time.deltaTime);
        }

        if (IsActive)
        {
            if (IsDetecting && !PlayerInAttackRange && CanMove && !IsStunned) //Si s'ha de moure o no
            {
                animator_rival.SetBool("Caminar", true);
                if (transform.position.x > PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else if (transform.position.x < PlayerPos.transform.position.x)
                {
                    transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
                    transform.localRotation = Quaternion.Euler(0, -180, 0);
                }

            }
            else if (IsDetecting && PlayerInAttackRange && !IsStunned) //Si esta al rang d'atac
            {
                animator_rival.SetBool("Caminar", false);

            }
        }
    }

    private void OnDie() //Morir i donar per acabada la missio
    {
        IsEnemyAlive = false;
        IsActive=false;
        endMissionOnceDead.EndOrAdvanceMission();
        CanMove = false;
        Vola = true;
        animator_rival.SetTrigger("RebreCop");
    }
    private void OnDrawGizmos() //Dibuixar cercles
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transformdetect.position, DetectingRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paret"))
        {
            Destroy(gameObject);
        }
    }

    public void FreezeBou()
    {
        CanMove = false;
    }
    public void UnfeezeBou()
    {
        CanMove = true;
    }
}
