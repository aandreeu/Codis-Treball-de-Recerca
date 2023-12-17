using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicModifyPlayerMoney : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory;
    public void ModifyPlayerMoney(float money)
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.ManipulateMoney(money);
    }

}
