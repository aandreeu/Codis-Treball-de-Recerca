using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvaaBulletSpawner : MonoBehaviour
{
    [SerializeField] float TimeForNextSpawn;
    [SerializeField] float RealTimer;

    [SerializeField] int BalesTotals;
    [SerializeField] int BalaActual;
    [SerializeField] int patroBales;
    [SerializeField] float variacioAngle;
    [SerializeField] float angleActual;
    [SerializeField] float[] anglesEscopetada;
    public bool disparaRafaga;
    [SerializeField] bool Anada;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform SpawnTransform;
    [SerializeField] Transform[] SpawnTransformSaltDispar;

    private void Start()
    {
        RealTimer = TimeForNextSpawn;
        BalaActual = 0;
        angleActual= 0;
        Anada = true;
    }
    private void Update()
    {
        RealTimer -= Time.deltaTime;

        if (patroBales == 1)
        {
            if (Anada && angleActual < -180f)
            {
                Anada = false;
            }
            else if (!Anada && angleActual > 0f)
            {
                Anada = true;
            }

            if (RealTimer <= 0 && disparaRafaga && !Anada)
            {
                angleActual = angleActual - variacioAngle;
                RealTimer = TimeForNextSpawn;
                SpawnBullet();
            }
            else if (RealTimer <= 0 && disparaRafaga && Anada)
            {
                angleActual = angleActual + variacioAngle;
                RealTimer = TimeForNextSpawn;
                SpawnBullet();
            }
        }
        else if (patroBales == 2)
        {
            if (RealTimer <= 0 && disparaRafaga)
            {
                RealTimer = TimeForNextSpawn;
                angleActual = anglesEscopetada[0];
                SpawnBullet();
                angleActual = anglesEscopetada[1];
                SpawnBullet();
                angleActual = anglesEscopetada[2];
                SpawnBullet();
            }

        }
    }
    public void SpawnBullet()
    {
        BalaActual++;
        GameObject bala = Instantiate(BulletPrefab, gameObject.transform) as GameObject;
        bala.transform.position = SpawnTransform.position;
        bala.GetComponent<IvaaBulletBehaviour>().DefineBullet(patroBales, angleActual);
        RealTimer = TimeForNextSpawn;
    }
    public void SpawnBulletSaltDispar(int _index)
    {
        if (_index == 0)
        {
            GameObject bala = Instantiate(BulletPrefab, gameObject.transform) as GameObject;
            bala.transform.position = SpawnTransformSaltDispar[0].position;
            bala.GetComponent<IvaaBulletBehaviour>().DefineBullet(2, -30f);
        }
        if (_index == 1)
        {
            GameObject bala = Instantiate(BulletPrefab, gameObject.transform) as GameObject;
            bala.transform.position = SpawnTransformSaltDispar[1].position;
            bala.GetComponent<IvaaBulletBehaviour>().DefineBullet(2, -90f);
        }
        if (_index == 2)
        {
            GameObject bala = Instantiate(BulletPrefab, gameObject.transform) as GameObject;
            bala.transform.position = SpawnTransformSaltDispar[2].position;
            bala.GetComponent<IvaaBulletBehaviour>().DefineBullet(2, -180f);
        }

    }
    public void ComencarRafaga()
    {
        disparaRafaga= true;
    }
    public void AcabarRafaga()
    {
        disparaRafaga= false;
    }
}
