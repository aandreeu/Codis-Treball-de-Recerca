using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public InventoryObject inventory;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadInventory()
    {
        inventory.Load();
    }
    public void SaveInventory()
    {
        inventory.Save();
    }
}
