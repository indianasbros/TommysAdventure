using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // This script manages the player's inventory, allowing items to be added when the player collides with them and interact with the item.
    private GameObject[] slots;
    int maxSlots = 0;
    bool canAddItem = true;
    bool isInventoryOpen = false;
    Item itemToAdd;
    [Header("Inventory UI")]
    [SerializeField]GameObject inventory;
    [SerializeField]GameObject inventorySlotHandler;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        itemToAdd = null; // Initialize itemToAdd to null
        slots = new GameObject[maxSlots];
        maxSlots = inventorySlotHandler.transform.childCount; // Get the number of child slots in the inventory slot handler
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
        Debug.Log("Player Inventory Initialized with " + maxSlots + " slots.");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
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
        if(Input.GetKeyDown(KeyCode.E) && canAddItem && itemToAdd != null) 
        {
            AddItem(itemToAdd.gameObject, itemToAdd.itemName, itemToAdd.description, itemToAdd.icon, itemToAdd.itemID);
            itemToAdd = null; // Clear the item to add after processing
            canAddItem = false; // Allow adding new items again
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
            interactable.canInteract = false; // Allow interaction with the item
            interactable.isInteracting = true; // Set the item as being interacted with
            interactable.ChangeToCamera(); // Change the camera to the item view if needed
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
