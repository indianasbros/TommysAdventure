 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && InventorySystem.Instance.CanAddItem)
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                InventorySystem.Instance.ItemToAdd = item;
            }
        }
        if (other.TryGetComponent(out Interactable interactable))
        {
            
            InventorySystem.Instance.InteractableObject = interactable; // Allow interaction with the previous item
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            InventorySystem.Instance.CanAddItem = false; // Disable adding items when exiting the trigger
            InventorySystem.Instance.ItemToAdd = null; // Clear the item to add when exiting the trigger
        }
        if (other.TryGetComponent(out Interactable interactable))
        {
            interactable.canInteract = true; // Allow interaction with the item
            InventorySystem.Instance.InteractableObject = null; // Clear the interactable object when exiting the trigger
        }
    }
    
}
