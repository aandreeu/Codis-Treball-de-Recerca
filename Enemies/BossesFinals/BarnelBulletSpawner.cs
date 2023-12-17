using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnelBulletSpawner : MonoBehaviour
{
    [SerializeField] float TimeForNextSpawn;
    [SerializeField] float RealTimer;

    [SerializeField] int BalesTotals;
    public int BalaActual;
    [SerializeField] int patroBales;
    [SerializeField] float variacioAngle;
    [SerializeField] float angleActual;
    public bool disparaRafaga;
    [SerializeField] float[] anglesEscopetada;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform SpawnTransform;
    [SerializeField] Transform[] PosicionsSpawn;
    [SerializeField] Animator BarnelAnimator;
    [SerializeField] BarnelBoss_Behaviour BarnelBossBehaviour;

    private void Start()
    {
        RealTimer = TimeForNextSpawn;
        BalaActual = 0;
    }
    private void Update()
    {
        if(disparaRafaga)
        {
            RealTimer -= Time.deltaTime;
        }

        if (BalaActual<15)
        {
            if (patroBales == 1)
            {
                if (RealTimer <= 0 && disparaRafaga)
                {
                    BarnelAnimator.SetTrigger("Disparar");
                    SpawnTransform = PosicionsSpawn[0];
                    angleActual = 180f;
                    SpawnBullet();
                }
            }
        }
        else if (BalaActual>=15 && BalaActual<38)
        {
            TimeForNextSpawn = 1f;
            if (patroBales == 1)
            {
                if (BalaActual%2==0 && RealTimer <= 0 && disparaRafaga)
                {
                    BarnelAnimator.SetTrigger("DispararAcotada");
                    SpawnTransform = PosicionsSpawn[1];
                    angleActual = 180f;
                    SpawnBullet();
                }
                else if (BalaActual % 2 == 1 && RealTimer <= 0 && disparaRafaga)
                {
                    BarnelAnimator.SetTrigger("Disparar");
                    SpawnTransform = PosicionsSpawn[0];
                    angleActual = 180f;
                    SpawnBullet();
                }
            }

        }
        else if (BalaActual>=BalesTotals)
        {
            disparaRafaga = false;
            BarnelAnimator.SetBool("SeguirDisparant", false);
            BarnelBossBehaviour.Unfreeze();
        }

    }
    public void SpawnBullet()
    {
        BalaActual++;
        GameObject bala = Instantiate(BulletPrefab, gameObject.transform) as GameObject;
        bala.transform.position = SpawnTransform.position;
        bala.GetComponent<BarnelBulletBehaviour>().DefineBullet(patroBales, angleActual);
        RealTimer = TimeForNextSpawn;
    }
    public void SpawnEscopetada()
    {
        if (!BarnelBossBehaviour.FacingRight)
        {
            angleActual = anglesEscopetada[0];
            SpawnBullet();
            angleActual = anglesEscopetada[1];
            SpawnBullet();
            angleActual = anglesEscopetada[2];
            SpawnBullet();
        }
        else
        {
            angleActual = anglesEscopetada[3];
            SpawnBullet();
            angleActual = anglesEscopetada[4];
            SpawnBullet();
            angleActual = anglesEscopetada[5];
            SpawnBullet();
        }
    }

    public void ComencarRafaga()
    {
        disparaRafaga = true;
    }
    public void AcabarRafaga()
    {
        disparaRafaga = false;
    }
}
