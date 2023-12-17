using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShotcutsDisplay : MonoBehaviour
{
    [SerializeField] int WhichItem;
    public PlayerInventory inventory;
    public BuffHandler buffHandler;

    [SerializeField] TMP_Text AmountDisplay;
    [SerializeField] Image SpriteDisplay;
    [SerializeField] Image TimerDisplay;

    private void Update()
    {
        //AmountDisplay.text = inventory.inventory.Container.Items[WhichItem].amount.ToString();

        //SpriteDisplay.sprite = inventory.inventory.database.GetItem[inventory.inventory.Container.Items[WhichItem].ID].uiDisplay;

        //if (inventory.inventory.Container.Items[WhichItem].item.buffs.Length > 0)
        {
          //  TimerDisplay.fillAmount = buffHandler.timer / inventory.inventory.Container.Items[WhichItem].item.buffs[0].EfectTime;
        }
    }
}
