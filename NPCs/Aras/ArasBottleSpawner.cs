using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArasBottleSpawner : MonoBehaviour
{
    [SerializeField] float TimeForNextSpawn;
    [SerializeField] float RealTimer;

    [SerializeField] int BalesTotals;
    public int BalaActual;
    [SerializeField] int patroBales;
    [SerializeField] float angleActual;
    public bool disparaRafaga;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform SpawnTransform;
    [SerializeField] Transform[] PosicionsSpawn;

    private void Start()
    {
        RealTimer = TimeForNextSpawn;
        BalaActual = 0;
    }
    private void Update()
    {
        if (disparaRafaga)
        {
            RealTimer -= Time.deltaTime;
        }

        if (BalaActual < 8)
        {
            if (patroBales == 1)
            {
                if (RealTimer <= 0 && disparaRafaga)
                {
                    SpawnTransform = PosicionsSpawn[0];
                    angleActual = 0;
                    SpawnBullet();
                }
            }
        }
        else if (BalaActual >= 8 && BalaActual < 20)
        {
            TimeForNextSpawn = 1f;
            if (patroBales == 1)
            {
                if (BalaActual % 2 == 0 && RealTimer <= 0 && disparaRafaga)
                {
                    SpawnTransform = PosicionsSpawn[1];
                    angleActual = 0;
                    SpawnBullet();
                }
                else if (BalaActual % 2 == 1 && RealTimer <= 0 && disparaRafaga)
                {
                    SpawnTransform = PosicionsSpawn[0];
                    angleActual = 0;
                    SpawnBullet();
                }
            }

        }
        else if (BalaActual >= BalesTotals)
        {
            disparaRafaga = false;
        }

    }
    public void SpawnBullet()
    {
        BalaActual++;
        GameObject bala = Instantiate(BulletPrefab, gameObject.transform) as GameObject;
        bala.transform.position = SpawnTransform.position;
        bala.GetComponent<AmpollaProjectil>().DefineBullet(patroBales, angleActual);
        RealTimer = TimeForNextSpawn;
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
