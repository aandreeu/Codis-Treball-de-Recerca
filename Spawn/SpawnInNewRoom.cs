using UnityEngine;

public class SpawnInNewRoom : MonoBehaviour
{
    public GameObject[] Spawn;
    public GameObject Player;

    private void Awake()
    {
        Spawn = GameObject.FindGameObjectsWithTag("Respawn");
        Player = GameObject.FindGameObjectWithTag("Player");

        Player.transform.position = Spawn[Player.GetComponent<BetweenScenes>().SpawnToTake].transform.position; //Pregunta quin spawn de la sala ha dagafar en cas que nhi hagi mes dun

        Player.SetActive(true);
        Player.GetComponent<PlayerController>().UnfreezePlayer();
    }
}
