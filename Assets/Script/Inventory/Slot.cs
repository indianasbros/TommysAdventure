using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool isEmpty = true;
    public ItemData itemData;
    public int quantity;

    public void SetItem(ItemData data, int qty = 1)
    {
        itemData = data;
        quantity = qty;
        isEmpty = false;
    }
    public void Update()
    {
        if (itemData != null)
        {
            GetComponent<Image>().sprite = itemData.icon;
        }
    }
    public void Clear()
    {
        itemData = null;
        quantity = 0;
        isEmpty = true;
    }
}
