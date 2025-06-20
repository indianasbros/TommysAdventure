using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableInventory : MonoBehaviour,IInventoryReceiver
{
    [Header("Puzzle Requirements")]
    public List<ItemRequirement> requiredItems = new List<ItemRequirement>();

    [Header("Puzzle Solved Event")]
    public UnityEvent onPuzzleResolved;

    
    public bool TryReceiveItems()
    {
        Debug.Log("recivo items");
        // Verificar si el inventario tiene todos los ítems necesarios
        foreach (var requirement in requiredItems)
        {
            int playerCount = InventorySystem.Instance.GetItemQuantity(requirement.item);
            if (playerCount < requirement.quantity)
            {
                Debug.Log($"Falta {requirement.item.itemName}: {requirement.quantity - playerCount} más.");
                return false;
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