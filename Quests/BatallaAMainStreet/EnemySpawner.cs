using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float TimeForNextSpawn;
    [SerializeField] float RealTimer;
    public int EnemiesLeftToSpawn;

    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] Transform SpawnTransform;

    private void Start()
    {
        RealTimer = TimeForNextSpawn;
        SpawnTransform = gameObject.transform;
    }

    private void Update()
    {
        RealTimer -= Time.deltaTime;

        if (RealTimer <= 0 && EnemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(EnemyPrefab, SpawnTransform);
        RealTimer = TimeForNextSpawn;
        EnemiesLeftToSpawn--;
    }

}
