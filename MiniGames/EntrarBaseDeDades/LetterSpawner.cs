using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public GameObject Letter_GO;
    public int WhichSpawner;
    [SerializeField] float MinTimeBetweenSpawns;
    [SerializeField] float MaxTimeBetweenSpawns;
    [SerializeField] float RealTimer;
    public LetterMGManager MG_Manager;

    private void Update()
    {
        if (MG_Manager.IsGamePlaying)
        {
            RealTimer -= Time.deltaTime;
            if (RealTimer < 0)
            {
                SpawnLetter();
            }
        }
    }
    public void SpawnLetter()
    {
        Instantiate(Letter_GO, gameObject.transform);
        RealTimer = Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns);
    }
}
