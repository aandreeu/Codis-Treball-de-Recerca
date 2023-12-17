using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] float MaxHealth;
    public float ActualHealth;
    public Transform RespawnPosition;
    [Header("Moviment Horitzontal")]
    private float horizontal;
    public float speed;
    public float PrivateSpeed;
    public bool isFacingRight = true;
    public bool canMove;
    [Header("Salt")]
    [SerializeField] private float jumpingPower;

    [Header("Esquivar")]
    public bool CanDash;
    public bool IsDashing;
    [SerializeField] private float DashPower;
    [SerializeField] private float DashTime;
    [SerializeField] private float DashCooldown;
    [SerializeField] bool IsCrouching;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BoxCollider2D ColliderDret;
    [SerializeField] private BoxCollider2D ColliderAjupit;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Contraatacar")]
    public bool IsSwordOut;
    public bool CanCounter;
    public bool IsCountering;
    public bool CanPressCounter; //Pot apretar
    public bool PressedInTime; //Un cop apretat
    [SerializeField] private float CounterPower;
    [SerializeField] private float CounterTime;

    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource playerAudioSource;
    [Header("Camera")]
    [SerializeField] private CinemachineConfiner cameraConfiner;

    private void Start()
    {
        canMove = true;
        DontDestroyOnLoad(gameObject);
        ActualHealth = MaxHealth;
        ColliderDret.enabled= true;
        ColliderAjupit.enabled= false;
    }

    void Update()
    {
        if (Input.GetKeyDown("z") && CanCounter && IsSwordOut && CanPressCounter)
        {
            PressedInTime = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded())
        {
            IsCrouching= true;
            playerAnimator.SetTrigger("Acotar");
            ColliderAjupit.enabled= true;
            ColliderDret.enabled = false;
            FreezePlayer();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && IsCrouching)
        {
            IsCrouching= false;
            playerAnimator.SetTrigger("Aixecar");
            ColliderAjupit.enabled= false;
            ColliderDret.enabled = true;
            Invoke("UnfreezePlayer", 0.1f);
        }

        if (IsDashing || IsCountering || IsCrouching)
        {
            return;
        }

        if(canMove) 
        {
            horizontal = Input.GetAxisRaw("Horizontal"); //Moviment Horitzontal
            playerAnimator.SetFloat("VelocitatCaminar", Mathf.Abs(horizontal));
            playerAnimator.SetBool("TocaTerra", IsGrounded());
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                playerAnimator.SetTrigger("Saltar");
            }

            if (Input.GetButtonUp("Jump") && _rigidbody2D.velocity.y > 0f) //Salt
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
            }
            Flip(); 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash && IsGrounded() && canMove)
        {
            playerAnimator.SetTrigger("Esquivar");
        }

        if (Input.GetKeyDown("p"))
        {
            TreureEspasa();
        }

    }

    private void FixedUpdate() //Fa avancar al ninot en funcio del valor donat a la variable horizontal
    {
        if (IsDashing || IsCountering)
        {
            return;
        }

        _rigidbody2D.velocity = new Vector2(horizontal * speed, _rigidbody2D.velocity.y);
    }

    private bool IsGrounded() //Toca el terra?
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() //Comprovar en quina direccio ha de mirar
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;
        MainDash(-DashPower);
        yield return new WaitForSeconds(DashTime);
        _rigidbody2D.gravityScale = 2f;
        IsDashing = false;
        yield return new WaitForSeconds(DashCooldown);
        CanDash = true;
    }
    private IEnumerator CounterAttack()
    {
        PressedInTime = false;
        CanCounter = false;
        IsCountering = true;
        MainDash(CounterPower);
        yield return new WaitForSeconds(CounterTime);
        _rigidbody2D.gravityScale = 2f;
        IsCountering = false;
    }
    public void StartCounterPermission()
    {
        CanPressCounter = true;
        CanCounter = true;
    }
    public void EndCounterPermission()
    {
        CanPressCounter = false;
        CanCounter = false;
    }
    public void CallCounterAttack()
    {
        if (PressedInTime)
        {
            playerAnimator.SetTrigger("Contraatacar");
        }
    }

    public void MainDash(float _power)
    {
        float originalGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(transform.localScale.x * _power, 0f);
    }
    public void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpingPower);
    }
    public void PlayerGetDamage(float HitDamage) //Rebre mal
    {
        ActualHealth -= HitDamage;
        StartCoroutine(ChangeColor());
        if (ActualHealth < 0)
        {
            AmagarEspasa();
            Invoke("OnDie", 1f);
        }
    }
    public void OnDie()
    {
        GameObject fade = GameObject.Find("FadeToBlack");
        fade.GetComponent<Animator>().Play("Fade_Death");
        Invoke("Revive", 2f);

    }
    private void Revive()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy1 in enemy)
        {
            if (enemy1.IsEnemyAlive)
            {
                enemy1.PlayerDied();
            }
        }
        gameObject.transform.position = RespawnPosition.position;
        ActualHealth = MaxHealth;
        playerAnimator.Play("Idle_Player");
        Invoke("TreureEspasa", 1f);
    }
    public void SetRespawnPosition(Transform RespawnPos)
    {
        RespawnPosition = RespawnPos;
    }
    private IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }


    public void FreezePlayer()
    {
        Debug.Log("Jugador Congelat");
        if(speed != 0)
        {
            PrivateSpeed = speed;
        }
        canMove = false;
        speed = 0f;
    }
    public void UnfreezePlayer()
    {
        speed = PrivateSpeed;
        canMove = true;
        Debug.Log("Jugador Descongelat");
    }

    public void TreureEspasa()
    {
        playerAnimator.SetBool("EspasaTreta", true);
        IsSwordOut = true;
        playerAnimator.Play("TreureEspasa_Player");
    }
    public void AmagarEspasa()
    {
        playerAnimator.SetBool("EspasaTreta", false);
        IsSwordOut = false;
        playerAnimator.Play("Idle_Player");
    }

    public void ExecutarSo(AudioClip so)
    {
        playerAudioSource.PlayOneShot(so);
    }
}