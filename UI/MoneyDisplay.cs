using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    public PlayerInventory playerInventory;
    [SerializeField] TMP_Text MoneyDisplayText;
    [SerializeField] float WaitingTime;

    private void Start()
    {
        MoneyDisplayText = gameObject.GetComponent<TMP_Text>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        MoneyDisplayText.text = playerInventory.CurrentMoney.ToString();
    }

    public void CridarAEnumerator() {
        StartCoroutine(ChangeMoneyText());
    }

    public IEnumerator ChangeMoneyText()
    {
        Debug.Log("Canvia Els Diners");
        char[] cha = MoneyDisplayText.text.ToCharArray();       // Borra el text anterior
        for (int i = MoneyDisplayText.text.Length-1; i >= 0; i--)
        {
            cha[i] = ' ';
            string newstring = new string(cha); 
            MoneyDisplayText.text=newstring;
            yield return new WaitForSeconds(WaitingTime);
        }
        MoneyDisplayText.text = string.Empty;
        foreach (char ch in playerInventory.CurrentMoney.ToString()) //Mostrar num per num els diners
        {
            MoneyDisplayText.text += ch;
            yield return new WaitForSeconds(WaitingTime);
        }

    }
}
