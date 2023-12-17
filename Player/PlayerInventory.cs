using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public float CurrentMoney;

    [SerializeField] GameObject Shotcut1Display;
    [SerializeField] GameObject Shotcut2Display;
    [SerializeField] MoneyDisplay moneyDisplay;

    public void ReciveItem() //Rebre un item i una quantitat
    {

        if (inventory.Container.Items[0]!=null && inventory.Container.Items[1] == null)
        {
            Shotcut1Display.SetActive(true);
            Debug.Log("Activa shotcut1");
        }   //activa el shotcut 1
        else if (inventory.Container.Items[0] != null && inventory.Container.Items[1] != null)
        {
            Shotcut2Display.SetActive(true);
            Debug.Log("Activa shotcut2");
        } //activa el shotcut 2
    }
    private void OnApplicationQuit() //Quan es surt de leditor de joc es borra l inventari
    {
        inventory.Container.Items = new InventorySlot[36];
    }

    public void ManipulateMoney(float ChargedMoney) //Cobrar els diners
    {
        CurrentMoney = CurrentMoney + ChargedMoney;
        moneyDisplay.CridarAEnumerator();
    }
}
