using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpwner_4Boss : MonoBehaviour
{
    [SerializeField] float TimeForNextSpawn;
    [SerializeField] float RealTimer;

    [SerializeField] int BalesTotals;
    [SerializeField] int BalaActual;

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Boss4_Behaviour boss4_Controller;
    [SerializeField] Animator BossAnimator;
    [SerializeField] string triggerName;
    [SerializeField] Transform SpawnTransform;

    private void Start()
    {
        RealTimer = TimeForNextSpawn;
        BalaActual = 0;
        SpawnTransform = gameObject.transform;
    }

    private void Update()
    {
        RealTimer -= Time.deltaTime;

        if (RealTimer <= 0 && BalaActual < BalesTotals)
        {
            BossAnimator.SetBool("SeguirDisparant", true);
            BossAnimator.SetTrigger(triggerName);
        }
        else if (BalaActual >= BalesTotals)
        {
            Invoke("ApagarBulletSpawner", 4f);
        }
    }

    private void ApagarBulletSpawner()
    {
        BossAnimator.SetBool("SeguirDisparant", false);
        boss4_Controller.Invoke("ApagarLoopsSang", 11f);
        BalaActual = 0;
        this.enabled = false;
    }

    public void SpawnBullet()
    {
        BalaActual++;
        GameObject bala = Instantiate(BulletPrefab, SpawnTransform) as GameObject;
        bala.GetComponent<BulletController>().DefineBullet();
        RealTimer = TimeForNextSpawn;
    }
}
