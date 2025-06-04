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
    Interactable interactableObject = null;
    Rigidbody rb;
    PlayerMovement playerMovement;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
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
        if (Input.GetKeyDown(KeyCode.E) && canAddItem && itemToAdd != null && interactableObject != null)
        {
            AddItem(itemToAdd.gameObject, itemToAdd.itemName, itemToAdd.description, itemToAdd.icon, itemToAdd.itemID);
            itemToAdd = null; // Clear the item to add after processing
            canAddItem = false; // Allow adding new items again

        }
        if (Input.GetKeyDown(KeyCode.E) && interactableObject != null)
        {
            if (!interactableObject.isInteracting)
            {
                Debug.Log("Interacting with object: " + interactableObject.name);
                interactableObject.isInteracting = true; // Set the interactable object to interacting state
                interactableObject.canInteract = false; // Disable further interactions with the item
                interactableObject.ChangeToCamera(); // Change the camera to the item view if needed
                animator.enabled = false; // Disable the player animator to prevent further interactions
                rb.isKinematic = true; // Disable physics interactions while interacting with the item
                playerMovement.enabled = false; // Disable player movement while interacting
            }
            else
            {
                Debug.Log("Already interacting with object: " + interactableObject.name);
                interactableObject.ChangeToMainCamera(); // Change back to the main camera
                interactableObject.isInteracting = false; // Reset the interacting state
                interactableObject.canInteract = true; // Re-enable interactions with the item
                animator.enabled = true; // Re-enable the player animator
                rb.isKinematic = false; // Re-enable physics interactions
                playerMovement.enabled = true; // Re-enable player movement
            }
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
            interactableObject = interactable; // Store the interactable object for later use
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
            interactableObject = null; // Clear the interactable object when exiting the trigger
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
