using UnityEngine;

[System.Serializable]
public class ItemRequirement
{
    public ItemData item;
    public int quantity;
    public ItemRequirement(ItemData itemInst, int qntt)
    {
        item = itemInst;
        quantity = qntt;
    }
}
