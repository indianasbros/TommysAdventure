using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickable
{
    public ItemData itemData;

    public void PickUp()
    {
        if (InventorySystem.Instance.TryAddItem(itemData))
        {
            gameObject.SetActive(false);
        }
    }
}
