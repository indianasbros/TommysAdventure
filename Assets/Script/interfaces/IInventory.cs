public interface IInventory
{
    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The data of the item to add.</param>
    /// <returns>True if the item was added successfully, false otherwise.</returns>
    bool TryAddItem(ItemData item, int amount = 1);

    /// <summary>
    /// Removes an item from the inventory.
    /// </summary>
    /// <param name="item">The data of the item to remove.</param>
    bool RemoveItem(ItemData item, int amount = 1);

    /// <summary>
    /// Checks if the inventory contains a specific item.
    /// </summary>
    /// <param name="item">The data of the item to check.</param>
    /// <returns>True if the item is in the inventory, false otherwise.</returns>
    bool HasItem(ItemData item);

    static InventorySystem Instance { get; private set; }
}