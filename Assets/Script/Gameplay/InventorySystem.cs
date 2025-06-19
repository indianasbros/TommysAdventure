using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    [Header("Inventory UI")]
    public GameObject inventoryUI;
    public GameObject inventorySlotHandler;
    private Slot[] slots;
    bool isInventoryOpen = false;

    private KeyCode inventaryKey = KeyCode.I;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        slots = inventorySlotHandler.GetComponentsInChildren<Slot>();

        //Control Setting for Inventary
        if (PlayerPrefs.HasKey("Key_3"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_3"), true, out var parsedKey))
            {
                inventaryKey = parsedKey;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(inventaryKey))
        {
            isInventoryOpen = !isInventoryOpen;
        }

        inventoryUI.SetActive(isInventoryOpen);

        
    }

    public bool TryAddItem(ItemData item)
    {
        foreach (var slot in slots)
        {
            if (!slot.isEmpty && slot.itemData == item && item.isStackable)
            {
                slot.quantity++;
                return true;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.SetItem(item, 1);
                slot.Update();
                return true;
            }
        }

        Debug.Log("Inventario lleno");
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
