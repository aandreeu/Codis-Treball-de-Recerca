using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnInteract : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject gameObjectToHide;

    void Start()
    {
        IsPlayerInRange = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController=FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && IsPlayerInRange)
        {
            if (gameObjectToHide.activeInHierarchy==true)
            {
                gameObjectToHide.SetActive(false);
                playerController.UnfreezePlayer();
            }
            else if (gameObjectToHide.activeInHierarchy==false)
            {
                gameObjectToHide.SetActive(true);
                playerController.FreezePlayer();
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //Si entra al rang, s activa IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Si surt del rang, es desactiva IsPlayerInRange
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }
}
