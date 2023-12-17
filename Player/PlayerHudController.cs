using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHudController : MonoBehaviour
{
    public GameObject Menu;
    public bool CanOpenMenu;
    public bool IsMenuOpened;
    public GameObject QuestMenuPanel;

    [SerializeField] PlayerController playerController;

    void Start()
    {
        CanOpenMenu = true;
        IsMenuOpened = false;
        playerController= GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CanOpenMenu) //Obrir menu
        {
            if (Menu.activeInHierarchy == true)
            {
                Menu.SetActive(false);
                playerController.UnfreezePlayer();
            }
            else if (Menu.activeInHierarchy == false)
            {
                Menu.SetActive(true);
                playerController.FreezePlayer();
                if (QuestMenuPanel.activeInHierarchy == true)
                {
                    QuestMenuPanel.SetActive(false);
                }
            }
        }
    }

    public void CantOpenMenu()
    {
        CanOpenMenu = false;
    }
    public void AbleOpenMenu()
    {
        CanOpenMenu = true;
    }

}
