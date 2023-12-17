using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Readable Item", menuName = "InventorySystem/Items/Readable")]
public class ReadableObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Readable;
    }

}
