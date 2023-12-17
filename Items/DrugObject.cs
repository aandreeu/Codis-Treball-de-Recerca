using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Drug Item", menuName = "InventorySystem/Items/Drugs")]
public class DrugObject : ItemObject
{
    private void Awake()
    {
        type=ItemType.Drug;
    }
}
