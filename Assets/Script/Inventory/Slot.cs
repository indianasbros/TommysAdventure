using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool isEmpty = true;
    public ItemData itemData;
    public int quantity;
    public Sprite defaultImage;
    public bool isPlayerInventorySlot = false;
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
        else
        {
            GetComponent<Image>().sprite = defaultImage;
        }
        
    }
    public void Clear()
    {
        itemData = null;
        quantity = 0;
        isEmpty = true;
    }
    public void OnPointerClick()
    {
        if (itemData == null)
        {
            return;
        }

        Debug.Log($"Clicked on slot with item: {itemData.itemName}, quantity: {quantity}");
        if (isPlayerInventorySlot && ObjectInventorySystem.Instance.IsInventoryOpen)
        {
            if (!itemData.isPowerUp)
            {
                // If the slot is a player inventory slot and the object inventory is open, show the context menu without deliver option
                ContextMenuController.Instance.Show(this, Input.mousePosition);
                return;
            }
            // If the slot is a player inventory slot and the object inventory is open, show the context menu with deliver option
            ContextMenuController.Instance.ShowWithDeliver(this, Input.mousePosition, InventorySystem.Instance, ObjectInventorySystem.Instance);
            return;
        }
        else if (!isPlayerInventorySlot && ObjectInventorySystem.Instance.IsInventoryOpen)
        {
            ContextMenuController.Instance.ShowWithDeliver(this, Input.mousePosition, ObjectInventorySystem.Instance, InventorySystem.Instance);
            return;
        }
        else if (isPlayerInventorySlot && !ObjectInventorySystem.Instance.IsInventoryOpen)
        {
            ContextMenuController.Instance.Show(this, Input.mousePosition);
            return;
        }
        
    }
}
