using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMGBaseDeDades : MonoBehaviour
{
    [SerializeField] int LineToAct;
    public Dialogues dialogues;
    [SerializeField] GameObject Mg_GO;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerHudController playerHudController;

    private void Update()
    {
        if (dialogues.lineIndex == LineToAct) //Quan la linea del dialeg coincideix amb LineToGive dona l item
        {
            playerController=FindObjectOfType<PlayerController>();
            playerHudController=FindObjectOfType<PlayerHudController>();
            playerController.FreezePlayer();
            playerHudController.CantOpenMenu();
            Mg_GO.SetActive(true);
            this.enabled = false;
        }
    }
}
