using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArasController : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] float MaxHealth;
    public float ActualHealth;
    [Header("Moviment Horitzontal")]
    private float horizontal;
    public float speed;
    public float PrivateSpeed;
    public bool isFacingRight = true;
    public bool canMove;
    [Header("Salt")]
    [SerializeField] private float jumpingPower;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        canMove = true;
        DontDestroyOnLoad(gameObject);
        ActualHealth = MaxHealth;
    }

    void Update()
    {
        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal"); //Moviment Horitzontal
            playerAnimator.SetFloat("VelocitatCaminar", Mathf.Abs(horizontal));
            playerAnimator.SetBool("TocaTerra", IsGrounded());
            Flip();
        }
    }

    private void FixedUpdate() //Fa avancar al ninot en funcio del valor donat a la variable horizontal
    {
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
    public void PlayerGetDamage(float HitDamage) //Rebre mal
    {
        ActualHealth -= HitDamage;
        StartCoroutine(ChangeColor());
        if (ActualHealth < 0)
        {
            Debug.Log("Mort");
        }
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
        if (speed != 0)
        {
            PrivateSpeed = speed;
        }
        canMove = false;
        speed = 0f;
    }
    public void UnfreezePlayer()
    {
        if(PrivateSpeed!= 0f)
        {
            speed = PrivateSpeed;
        }
        canMove = true;
        Debug.Log("Jugador Descongelat");
    }

}
