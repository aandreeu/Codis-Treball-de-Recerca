using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInShopTemplate : MonoBehaviour
{
    public TMP_Text ItemTitle;
    public TMP_Text ItemDescription;
    public Image ItemIcon;
    public TMP_Text ItemPrice;
    public Item item;
    public ItemObject ItemObject;

    public PlayerInventory PlayerInventory;
    public InventoryObject inventoryObject;

    public void Awake() //Definir els textos i imatges
    {
        PlayerInventory = FindObjectOfType<PlayerInventory>();

        ItemTitle.text = ItemObject.name;
        ItemDescription.text = ItemObject.description;
        ItemIcon.sprite = ItemObject.uiDisplay;
        ItemPrice.text = ItemObject.price + "€";
    }
    public void Purchase() //Al apretar el boto restem el preu de l item als diners que tingui. tambe donem l item
    {
        if (PlayerInventory.CurrentMoney >= ItemObject.price)
        {
            PlayerInventory.ManipulateMoney(-ItemObject.price);
            inventoryObject.AddItem(item, 1);
        }
        else
        {
            Debug.Log("No");
        }
    }
}
