using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    bool canAddItem;
    [HideInInspector] public Item itemToAdd;
    private bool isInteracting;
    public bool IsInteracting
    {
        get { return isInteracting; }
        set
        {
            isInteracting = value;
        }
    }
    [HideInInspector] public Interactable interactableObject = null;
    // Start is called before the first frame update
    void Start()
    {
        itemToAdd = null; // Initialize itemToAdd to null
        canAddItem = true;
        isInteracting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.E) && interactableObject != null)
        {
            Debug.Log("Interacting with object: " + interactableObject.name+ " at position: " + interactableObject.transform.position);
            Debug.Log("gamemanager instance: " + GameplayManager.Instance);
            GameplayManager.Instance.InteractWithObject(interactableObject.gameObject); // Call the interact method in GameplayManager
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
    
}
