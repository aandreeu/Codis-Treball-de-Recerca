using UnityEngine;

public class GiveItemThroughDialogue : MonoBehaviour
{
    public Item item;
    [SerializeField] int itemamount;
    public int LineToGive;
    public Dialogues dialogues;
    public PlayerInventory playerInventory;
    public InventoryObject inventoryObject;

    private void Start()
    {
        dialogues= gameObject.GetComponent<Dialogues>();
        playerInventory= FindObjectOfType<PlayerInventory>();
    }
    private void Update()
    {
        if(dialogues.lineIndex == LineToGive) //Quan la linea del dialeg coincideix amb LineToGive dona l item
        {
            playerInventory.ReciveItem();
            inventoryObject.AddItem(item, itemamount);
            this.enabled = false;
        }
    }
}
