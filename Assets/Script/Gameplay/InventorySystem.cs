using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public PlayerInventory player;
    private static InventorySystem _instance;
    public static InventorySystem Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("InventorySystem");
                _instance = obj.AddComponent<InventorySystem>();
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // This script manages the player's inventory, allowing items to be added when the player collides with them and interact with the item.
    private GameObject[] slots;
    int maxSlots = 0;
    bool canAddItem = true;
    bool isInventoryOpen = false;
    Item itemToAdd;
    private bool isInteracting = false;
    public bool IsInteracting
    {
        get { return isInteracting; }
        set
        {
            isInteracting = value;
        }
    }

    [Header("Inventory UI")]
    [SerializeField]GameObject inventory;
    [SerializeField]GameObject inventorySlotHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in InventorySystem.");
            GameObject playerTemp = GameObject.FindWithTag("Player");

            if (player == null)
            {
                Debug.LogError("No GameObject with tag 'Player' found in the scene.");
                return;
            }
            playerTemp.TryGetComponent(out PlayerInventory playerInventory);
            if (playerInventory == null)
            {
                
                Debug.LogError("Player GameObject does not have a PlayerInventory component.");
                return;
            }
            player = playerInventory; // Assign the found PlayerInventory component to the player variable
        }
        else
        {
            Debug.Log("InventorySystem initialized with player: " + player.name);
        }
        itemToAdd = null; // Initialize itemToAdd to null
        maxSlots = inventorySlotHandler.transform.childCount; // Get the number of child slots in the inventory slot handler
        slots = new GameObject[maxSlots];
        if (maxSlots <= 0)
        {
            Debug.LogError("No slots found in the inventory slot handler. Please ensure there are child objects representing slots.");
            return;
        }
        for (int i = 0; i < maxSlots; i++)
        {
            slots[i] = inventorySlotHandler.transform.GetChild(i).gameObject; // Assign each child slot to the slots array
            if(slots[i].GetComponent<Slot>().item == null)
            {
                slots[i].GetComponent<Slot>().isEmpty = true; // Mark the slot as empty if it has no item
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
        }
        if (isInventoryOpen)
        {
            //Debug.Log("Inventory Opened");
            inventory.SetActive(isInventoryOpen);
        }
        else
        {
            //Debug.Log("Inventory Closed");
            inventory.SetActive(isInventoryOpen);

        }
        if (Input.GetKeyDown(KeyCode.E) && canAddItem && itemToAdd != null && player.interactableObject != null)
        {
            AddItem(itemToAdd.gameObject, itemToAdd.itemName, itemToAdd.description, itemToAdd.icon, itemToAdd.itemID);
            itemToAdd = null; // Clear the item to add after processing
            canAddItem = false; // Allow adding new items again

        }
        if (Input.GetKeyDown(KeyCode.E) && player.interactableObject != null)
        {
            Debug.Log("Interacting with object: " + player.interactableObject.name+ " at position: " + player.interactableObject.transform.position);
            Debug.Log("gamemanager instance: " + GameplayManager.Instance);
            GameplayManager.Instance.InteractWithObject(player.interactableObject.gameObject); // Call the interact method in GameplayManager
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && canAddItem)
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                canAddItem = true;
                itemToAdd = item;
            }
        }
        if (other.TryGetComponent(out Interactable interactable))
        {
            player.interactableObject = interactable; // Store the interactable object for later use
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            canAddItem = false; // Disable adding items when exiting the trigger
            itemToAdd = null; // Clear the item to add when exiting the trigger
        }
        if (other.TryGetComponent(out Interactable interactable))
        {
            interactable.canInteract = true; // Allow interaction with the item
            player.interactableObject = null; // Clear the interactable object when exiting the trigger
        }
    }
    public void AddItem(GameObject item, string itemName, string description, Sprite icon, int itemID)
    {
        // This method can be called to add an item to the inventory.
        // It can be used for adding items programmatically or from other scripts.
        if (itemToAdd != null)
        {
            Debug.LogWarning("An item is already being added. Please wait until the current item is processed.");
            return;
        }


        for (int i = 0; i < maxSlots; i++)
        {
            if (slots[i].GetComponent<Slot>().isEmpty)
            {
                item.GetComponent<Item>().pickedUp = true;

                slots[i].GetComponent<Slot>().item = item;
                slots[i].GetComponent<Slot>().itemName = itemName;
                slots[i].GetComponent<Item>().icon = icon;
                slots[i].GetComponent<Item>().description = description;
                slots[i].GetComponent<Item>().itemID = itemID;

                itemToAdd.transform.parent = slots[i].transform; // Set the item's parent to the slot    
                itemToAdd.gameObject.SetActive(false); // Deactivate the item in the scene

                slots[i].GetComponent<Slot>().isEmpty = false;
                break; // Exit the loop once a slot is found
            }
        }


        Debug.Log("Item added: " + itemName);
        // Logic to add the item to the inventory UI can be added here
    }
}
