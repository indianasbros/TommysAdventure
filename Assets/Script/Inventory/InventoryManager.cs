using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public ItemData heldItem;
    public int heldQuantity;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public bool IsHoldingItem => heldItem != null;

    public void PickUpItem(ItemData item, int quantity)
    {
        heldItem = item;
        heldQuantity = quantity;
        Debug.Log($"Picked up {item.itemName} x{quantity}");
    }

    public void PlaceHeldItem(Slot slot)
    {
        if (!IsHoldingItem) return;

        if (slot.isEmpty)
        {
            slot.SetItem(heldItem, heldQuantity);
            ClearHeldItem();
        }
        else if (slot.itemData == heldItem && heldItem.isStackable)
        {
            slot.quantity += heldQuantity;
            ClearHeldItem();
        }
        else
        {
            // Swap
            var tempItem = slot.itemData;
            var tempQty = slot.quantity;
            slot.SetItem(heldItem, heldQuantity);
            PickUpItem(tempItem, tempQty);
        }

        slot.Update();
    }

    public void ClearHeldItem()
    {
        heldItem = null;
        heldQuantity = 0;
    }
    public void TryDeliverItem(Slot slot)
    {
        if (slot.isEmpty) return;

        var player = InteractSystem.Instance.player;
        var detector = InteractSystem.Instance.targetDetector;
        var receiver = detector?.GetComponentInChildren<IInventoryReceiver>();

        if (receiver is InteractableInventory interactable)
        {
            bool success = interactable.TryReceiveItem(slot.itemData);
            if (success)
            {
                slot.Clear();
                slot.Update();
            }
        }
        else
        {
            Debug.Log("No hay objeto cerca para recibir ítems.");
        }
    }
}
