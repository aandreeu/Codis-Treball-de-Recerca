using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive_Shop : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject ShopPanel;
    [SerializeField] GameObject PanelE;
    [SerializeField] Animator Animator_PressE;
    [SerializeField] bool IsPlayerInRange;

    private void Update()
    {
        if (IsPlayerInRange && Input.GetKeyDown("e"))
        {
            ShopPanel.SetActive(true);
            playerController.FreezePlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = FindObjectOfType<PlayerController>();
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = true;
            PanelE.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            Animator_PressE.SetBool("HasToLoop", false);
        }
    }

    public void HideShop()
    {
        ShopPanel.SetActive(false);
        playerController.UnfreezePlayer();
    }
}
