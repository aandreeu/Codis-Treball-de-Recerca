using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [SerializeField] float LaserDamage;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] EdgeCollider2D colliderLinea;
    [SerializeField] Texture[] textures;
    [SerializeField] Boss4_Behaviour boss4Controller;
    [SerializeField] int animationStep;
    [SerializeField] float fps = 30f;
    private float fpsCounter;
    [SerializeField] GameObject player;
    public Transform firePoint_DavantBaix;
    public Transform PlayerPoint;
    public Transform ParetEsqPoint;
    public Transform ParetDretaPoint;
    public Transform firePoint_DavantAlt;    
    public Transform firePoint_DavantMig;
    public Transform finalPoint_Alt;
    public Transform finalPoint_Mig;

    public void ApuntarLaserJugador()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
        lineRenderer.SetPosition(0, firePoint_DavantBaix.position);
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerPoint = player.transform;
        lineRenderer.SetPosition(1, PlayerPoint.position);
        Debug.Log(PlayerPoint.position.x.ToString() + PlayerPoint.position.x.ToString());
        //animator.Play("LoadLaser");
        Invoke("LaserDealDamage", 2f);
    }
    public void ApuntarParetEsquerra()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
        lineRenderer.SetPosition(0, firePoint_DavantBaix.position);
        lineRenderer.SetPosition(1, ParetEsqPoint.position);
        Invoke("LaserDealDamage", 2f);
    }
    public void ApuntarAlt()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
        lineRenderer.SetPosition(0, firePoint_DavantAlt.position);
        lineRenderer.SetPosition(1, finalPoint_Alt.position);
        Invoke("LaserDealDamage", 2f);
    }
    public void ApuntarMig()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
        lineRenderer.SetPosition(0, firePoint_DavantMig.position);
        lineRenderer.SetPosition(1, finalPoint_Mig.position);
        Invoke("LaserDealDamage", 2f);
    }
    public void ApuntarParetQueApunta()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
        if (boss4Controller.FacingRight)
        {
            lineRenderer.SetPosition(0, firePoint_DavantBaix.position);
            lineRenderer.SetPosition(1, ParetDretaPoint.position);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint_DavantBaix.position);
            lineRenderer.SetPosition(1, ParetEsqPoint.position);
        }
        Invoke("LaserDealDamage", 2f);
    }

    private void Update()
    {
        if (gameObject.GetComponent<LineRenderer>().enabled == true)
        {
            fpsCounter += Time.deltaTime;
            if (fpsCounter > 1f / fps)
            {
                animationStep++;
                if (animationStep == textures.Length)
                {
                    animationStep = 6;
                    fps = 30;
                }
                lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);
                fpsCounter = 0f;
            }
        }
        else
        {
            animationStep = 0;
            fps = 15;
        }
    }

    public void LaserDealDamage() //Cridada des de l animator quan el tingui
    {
        colliderLinea.enabled = true;
        colliderLinea.points[0].x= lineRenderer.GetPosition(0).x;
        colliderLinea.points[0].y= lineRenderer.GetPosition(0).y;
        colliderLinea.points[1].x= lineRenderer.GetPosition(1).x;
        colliderLinea.points[1].y= lineRenderer.GetPosition(1).y;

        Debug.Log("LaserAtaca");
        Invoke("ApagarLaser", 0.2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.PlayerGetDamage(LaserDamage);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void ApagarLaser()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
        gameObject.GetComponent<EdgeCollider2D>().enabled = false;

    }
   
}
