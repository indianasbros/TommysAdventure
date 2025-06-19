using UnityEngine;

public class ItemPickup: MonoBehaviour, IPickable
{
    public ItemData itemData;
    [SerializeField] bool destroyOnPickUp;

    public void PickUp()
    {
        if (InventorySystem.Instance.TryAddItem(itemData))
        {
            if (destroyOnPickUp)
            {
                Destroy(gameObject);
                return;
            }
            gameObject.SetActive(false);
        }
    }
}
