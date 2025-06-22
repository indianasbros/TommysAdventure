using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableInventory : MonoBehaviour,IInventoryReceiver
{
    [Header("Puzzle Requirements")]
    public List<ItemRequirement> requiredItems;

    [Header("Puzzle Solved Event")]
    public UnityEvent onPuzzleResolved;
    bool isInventoryOpen = false;
    public bool IsInventoryOpen => isInventoryOpen;
    bool canReceive = true;
    public bool CanReceive { get => canReceive; set => canReceive = value; }

    void Start()
    {
        canReceive = true; 
    }
    
    public bool TryReceiveItem(ItemData item)
    {
        foreach (var requirement in requiredItems)
        {
            if (requirement.item == item && InventorySystem.Instance.GetItemQuantity(item) >= requirement.quantity)
            {
                ObjectInventorySystem.Instance.TryAddItem(item, requirement.quantity);
                InventorySystem.Instance.RemoveItem(item, requirement.quantity);
                Debug.Log("Item entregado correctamente al objeto");

                onPuzzleResolved?.Invoke();
                return true;
            }
        }

        Debug.Log("Este item no cumple con los requisitos o cantidad insuficiente");
        return false;
    }
    public void ConsumeItem(ItemData item, int amount = 1)
    {
        requiredItems.RemoveAt(
            requiredItems.FindIndex(requirement => requirement.item == item && requirement.quantity <= amount)
        );
        if (requiredItems.Count == 0)
        {
            onPuzzleResolved?.Invoke();
            CanReceive = false;
            Debug.Log("Todos los requisitos del puzzle han sido cumplidos.");
        }
    }
}