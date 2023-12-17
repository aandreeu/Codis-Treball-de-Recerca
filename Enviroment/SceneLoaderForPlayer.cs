using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoaderForPlayer : MonoBehaviour
{
    [SerializeField] int SceneIndexToLoad;
    [SerializeField] bool IsPlayerInRange;
    public bool CanPlayerTp;
    [SerializeField] int PlayerNextSpawn;
    [SerializeField] GameObject Player;
    [SerializeField] float PlayerSpeedPostTp_Aut;
    [SerializeField] Animator FadeToBlack;
    [SerializeField] private GameObject PanelE;
    [SerializeField] private Animator Animator_PressE;

    void Start()
    {
        IsPlayerInRange = false;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (IsPlayerInRange && Input.GetKey("e") && CanPlayerTp)
        {                    
           FadeToScene();            
        }
    }

    public void LoadNewScene()//Cridat per l animator
    {
        if(Player.GetComponent<PlayerController>().speed<=0)
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
        SceneIndexToLoad= _sceneIndexToLoad;
    }

    public void UnfreezePlayerOnTp(float _speed)
    {
        Player.GetComponent<PlayerController>().UnfreezePlayer();
        //Player.GetComponent<PlayerController>().speed = _speed;
    }

    public void ChangeCanPlayerTp(bool TrueFalse)
    {
        CanPlayerTp=TrueFalse;
    }

    private void OnTriggerEnter2D(Collider2D collision)//Si entra al rang, sactiva IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = true;
            if (CanPlayerTp)
            {
                PanelE.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//Si surt del rang, es desactiva IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            if (CanPlayerTp)
            {
                Animator_PressE.SetBool("HasToLoop", false);
            }

        }
    }

}
