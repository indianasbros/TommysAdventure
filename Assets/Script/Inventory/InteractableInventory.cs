using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableInventory : MonoBehaviour,IInventoryReceiver
{
    [Header("Puzzle Requirements")]
    public List<ItemRequirement> requiredItems = new List<ItemRequirement>();
    private List<ItemRequirement> items = new List<ItemRequirement>();

    [Header("Puzzle Solved Event")]
    public UnityEvent onPuzzleResolved;

    public bool ReceiveItem(ItemData itemToReceive)
    {
        foreach (var itemRequirement in items)
        {
            if (itemRequirement.item == itemToReceive)
            {
                Debug.Log("Ya cuenta con este item");
                return false;
            }
        }
        foreach (var requirement in requiredItems)
        {
            
            if (itemToReceive == requirement.item)
            {
                items.Add(new ItemRequirement(requirement.item, 1));
                return false;
            }
        }
        return true;
    }
    public bool TryReceiveItems()
    {
        // Verificar si el inventario tiene todos los ítems necesarios
        foreach (var requirement in requiredItems)
        {
            int playerCount = InventorySystem.Instance.GetItemQuantity(requirement.item);
            if (playerCount < requirement.quantity)
            {
                Debug.Log($"Falta {requirement.item.itemName}: {requirement.quantity - playerCount} más.");
                return false;
            }
            else
            {
                items.Add(new ItemRequirement(requirement.item,requirement.quantity));
            }
        }

        // Quitar los ítems requeridos
        foreach (var requirement in requiredItems)
        {
            InventorySystem.Instance.RemoveItem(requirement.item, requirement.quantity);
        }

        Debug.Log("Puzzle resuelto!");
        onPuzzleResolved?.Invoke();
        return true;
    }
}