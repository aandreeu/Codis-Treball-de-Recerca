using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneLoader : MonoBehaviour
{
    [SerializeField] int SceneIndexToLoad;
    [SerializeField] bool IsPlayerInRange;
    public bool CanPlayerTp;
    [SerializeField] int PlayerNextSpawn;
    [SerializeField] GameObject Player;
    [SerializeField] float PlayerSpeedPostTp_Aut;
    [SerializeField] Animator FadeToBlack;

    void Start()
    {
        IsPlayerInRange = false;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LoadNewScene()//Cridat per l animator
    {
        if (Player.GetComponent<PlayerController>().speed <= 0)
        {
            UnfreezePlayerOnTp(PlayerSpeedPostTp_Aut);
        }

        Player.GetComponent<BetweenScenes>().SpawnToTake = PlayerNextSpawn;
        SceneManager.LoadScene(SceneIndexToLoad);
    }

    public void FadeToScene()
    {
        Player.GetComponent<PlayerController>().FreezePlayer();
        FadeToBlack.SetTrigger("FadeOut");
        Invoke("LoadNewScene", 1);
    }

    public void ChangeSceneIndexToLoad(int _sceneIndexToLoad)
    {
        SceneIndexToLoad = _sceneIndexToLoad;
    }

    public void UnfreezePlayerOnTp(float _speed)
    {
        Player.GetComponent<PlayerController>().UnfreezePlayer();
        //Player.GetComponent<PlayerController>().speed = _speed;
    }

    public void ChangeCanPlayerTp(bool TrueFalse)
    {
        CanPlayerTp = TrueFalse;
    }

    private void OnTriggerEnter2D(Collider2D collision)//Si entra al rang, sactiva IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            FadeToScene();
        }
    }

}
