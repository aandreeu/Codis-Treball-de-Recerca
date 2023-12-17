using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLaser_Spawner : MonoBehaviour
{
    [SerializeField] float TimeForNextSpawn;
    [SerializeField] float RealTimer;
    [SerializeField] GameObject Laser;

    private void Update()
    {
        RealTimer -= Time.deltaTime;

        if (RealTimer <= 0)
        {
            SpawnLaser();
        }
    }

    public void SpawnLaser()
    {
        Laser.SetActive(true);
        RealTimer = TimeForNextSpawn;
    }


}
