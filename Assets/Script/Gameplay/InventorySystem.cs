using System;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    [Header("Inventory UI")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inventorySlotHandler;
    private Slot[] slots;
    public Slot[] Slots => slots;
    bool isInventoryOpen = false;
    public static InventorySystem Instance { get; private set; }
    public event Action<Slot[]> OnUpdateInventory;
    private KeyCode inventoryKey = KeyCode.I;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        slots = inventorySlotHandler.GetComponentsInChildren<Slot>();

        //Control Setting for Inventary
        if (PlayerPrefs.HasKey("Key_3"))
        {
            if (Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_3"), true, out var parsedKey))
            {
                inventoryKey = parsedKey;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(inventoryKey) && !InteractSystem.Instance.IsInteracting && !ObjectInventorySystem.Instance.IsInventoryOpen)
        {
            isInventoryOpen = !isInventoryOpen;
            if (isInventoryOpen)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    
    }
    public void OpenInventory()
    {
        CameraManager.Instance.LockCursor(false);
        InteractSystem.Instance.player.GetComponent<PlayerMovement>().FreezeMovement = true;
        inventoryUI.SetActive(true);
        
    }
    public void CloseInventory()
    {
        CameraManager.Instance.LockCursor(true);
        InteractSystem.Instance.player.GetComponent<PlayerMovement>().FreezeMovement = false;
        inventoryUI.SetActive(false);
    }

    public bool TryAddItem(ItemData item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (!slot.isEmpty && slot.itemData == item && item.isStackable)
            {
                slot.quantity += amount;
                OnUpdateInventory?.Invoke(slots);
                return true;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.SetItem(item, 1);
                slot.Update();
                OnUpdateInventory?.Invoke(slots);
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(ItemData item, int amount = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];
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
        foreach (var slot in slots)
        {
            if (!slot.isEmpty && slot.itemData == item)
                return true;
        }
        return false;
    }
    public int GetItemQuantity(ItemData item)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            if (!slot.isEmpty && slot.itemData == item)
            {
                total += slot.quantity;
            }
        }
        return total;
    }
}
