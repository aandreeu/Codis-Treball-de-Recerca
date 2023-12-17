using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Drug,
    Readable
}

public enum Attributes
{
    Health,
    Speed,
    Strenght,

}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(3, 5)]
    public string description;
    public float price;
    public ItemBuff[] buffs;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].mult, item.buffs[i].EfectTime)
            {
                attribute = item.buffs[i].attribute
            };
        }
    }
}
[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public float mult;
    public float EfectTime;
    public ItemBuff(float _mult, float _EfectTime)
    {
        mult = _mult;
        EfectTime= _EfectTime;
    }
}