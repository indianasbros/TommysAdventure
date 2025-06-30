using UnityEngine;
using System;
public class ObjectInventorySystem : MonoBehaviour
{

    [Header("Inventory UI")]
    [SerializeField] private GameObject playerInventorySlotHandler;
    [SerializeField] private GameObject objectInventoryUI;
    [SerializeField] private GameObject objectInventorySlotHandler;
    private Slot[] playerSlots;
    private Slot[] objectSlots;
    InteractableInventory currentObjectInventory;
    bool isInventoryOpen = false;
    public bool IsInventoryOpen => isInventoryOpen;
    public static ObjectInventorySystem Instance { get; private set; }

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }
    public void ClearInventory()
    {
        if (currentObjectInventory == null) return;
        currentObjectInventory = null;
        foreach (var slot in objectSlots)
        {
            slot.Clear();
            slot.Update();
        }
    }
    private void UpdateObjectInventory(InteractableInventory interactable)
    {
        currentObjectInventory = interactable;
        if (currentObjectInventory == null) return;
 
    }
    private void UpdatePlayerInventory(Slot[] slots)
    {
        if (slots == null || slots.Length == 0) return;
        foreach (var slot in slots)
        {
            if (slot == null) continue;
            if (slot.isEmpty) continue;
            if (slot.itemData == null) continue;
            if (slot.isPlayerInventorySlot)
            {
                foreach (var playerSlot in playerSlots)
                {
                    if (playerSlot.isEmpty && playerSlot.itemData == null)
                    {
                        playerSlot.SetItem(slot.itemData, slot.quantity);
                        playerSlot.Update();
                        return; // Exit after moving the first item
                    }
                }
                Debug.Log("No empty player inventory slots available to move item.");
            }
        }
    }
    
    void Start()
    {
        InteractSystem.Instance.OnCanInteractWithInventory += UpdateObjectInventory;
        InventorySystem.Instance.OnUpdateInventory += UpdatePlayerInventory;
        objectSlots = objectInventorySlotHandler.GetComponentsInChildren<Slot>();
        objectInventoryUI.SetActive(false);
        foreach (var slot in objectSlots)
        {
            slot.isPlayerInventorySlot = false;
        }
        playerSlots = playerInventorySlotHandler.GetComponentsInChildren<Slot>();
        foreach (var slot in playerSlots)
        {
            slot.isPlayerInventorySlot = true;
        }
    }
    public void OpenInventory()
    {
        Debug.Log("Opening Object Inventory");
        CameraManager.Instance.LockCursor(false);
        InteractSystem.Instance.player.GetComponent<PlayerMovement>().FreezeMovement = true;
        isInventoryOpen = true;
        objectInventoryUI.SetActive(true);
        ContextMenuController.Instance.Hide();
        ItemDescriptionUI.Instance.Hide();

    }
    public void CloseInventory()
    {
        CameraManager.Instance.LockCursor(true);
        InteractSystem.Instance.player.GetComponent<PlayerMovement>().FreezeMovement = false;
        isInventoryOpen = false;
        ContextMenuController.Instance.Hide();
        ItemDescriptionUI.Instance.Hide();
        currentObjectInventory = null;
        objectInventoryUI.SetActive(false);
    }

    public bool TryAddItem(ItemData item, int amount = 1)
    {
        foreach (var slot in objectSlots)
        {
            if (!slot.isEmpty && slot.itemData == item && item.isStackable)
            {
                slot.quantity += amount;
                return true;
            }
        }

        foreach (var slot in objectSlots)
        {
            if (slot.isEmpty)
            {
                slot.SetItem(item, 1);
                slot.Update();
                if (InRequiredItems())
                {
                    ConsumeRequiredItem(item, amount);
                }
                return true;
            }
        }

        Debug.Log("Inventario lleno");
        return false;
    }

    public bool RemoveItem(ItemData item, int amount = 1)
    {
        for (int i = 0; i < objectSlots.Length; i++)
        {
            var slot = objectSlots[i];
            if (!slot.isEmpty && slot.itemData == item)
            {
                if (slot.quantity >= amount)
                {
                    slot.quantity -= amount;
                    if (slot.quantity == 0)
                    {
                        slot.Clear();
                        slot.Update();
                    }
                    return true;
                }
                else
                {
                    amount -= slot.quantity;
                    slot.Clear();
                    slot.Update();
                }
            }
        }
        return amount <= 0;
    }
    public bool HasItem(ItemData item)
    {
        foreach (var slot in objectSlots)
        {
            if (!slot.isEmpty && slot.itemData == item)
                return true;
        }
        return false;
    }
    public int GetItemQuantity(ItemData item)
    {
        int total = 0;
        foreach (var slot in objectSlots)
        {
            if (!slot.isEmpty && slot.itemData == item)
            {
                total += slot.quantity;
            }
        }
        return total;
    }
    private bool InRequiredItems()
    {
        if (currentObjectInventory == null) return false;
        foreach (var requirement in currentObjectInventory.requiredItems)
        {
            if (requirement.item == null) continue;
            if (GetItemQuantity(requirement.item) >= requirement.quantity)
            {
                return true;
            }
        }
        return false;
    }
    private void ConsumeRequiredItem(ItemData item, int amount)
    {
        if (currentObjectInventory == null) return;
        foreach (var requirement in currentObjectInventory.requiredItems)
        {
            if (requirement.item == item && GetItemQuantity(item) >= requirement.quantity)
            {
                RemoveItem(item, amount);
                currentObjectInventory.ConsumeItem(item, amount);
                InventorySystem.Instance.RemoveItem(item, amount);
                if (currentObjectInventory.requiredItems.Count == 0)
                {
                    currentObjectInventory.onPuzzleResolved?.Invoke();
                }
                
                return;
            }
        }
    }

}
