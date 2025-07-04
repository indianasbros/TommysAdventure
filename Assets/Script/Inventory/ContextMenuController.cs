using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    public GameObject menuRoot;
    public Button deliverButton;
    public Button discardButton;
    public Button descriptionButton;
    private Slot currentSlot;
    private float lastTimeClicked;
    public static ContextMenuController Instance { get; private set; }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        menuRoot.SetActive(false);
    }

    public void ShowWithDeliver(Slot slot, Vector2 position, IInventory inventoryFrom, IInventory inventoryTo)
    {
        currentSlot = slot;
        menuRoot.SetActive(true);
        menuRoot.transform.position = position;

        deliverButton.onClick.RemoveAllListeners();
        discardButton.onClick.RemoveAllListeners();
        deliverButton.gameObject.SetActive(true);
        deliverButton.onClick.AddListener(() =>
        {
            if(Time.time - lastTimeClicked > 0.5f)
            {
                inventoryTo.TryAddItem(currentSlot.itemData, currentSlot.quantity);
                inventoryFrom.RemoveItem(currentSlot.itemData, currentSlot.quantity);
                Hide();
                lastTimeClicked = Time.time; // Update last clicked time
                return; // Prevent double clicks
            }
        });

        discardButton.onClick.AddListener(() =>
        {
            if(Time.time - lastTimeClicked > 0.5f)
            {
                inventoryFrom.RemoveItem(currentSlot.itemData, currentSlot.quantity);
                Hide();
                if (currentSlot.itemData.isPowerUp && PowerUps.Instancia.HasPowerUp(currentSlot.itemData))
                {
                    TimeController.Instance.DiscartPowerUp();
                }
                lastTimeClicked = Time.time; // Update last clicked time
                return; // Prevent double clicks
            }
        });
        descriptionButton.onClick.RemoveAllListeners();
        descriptionButton.onClick.AddListener(() =>
        {
            if(Time.time - lastTimeClicked > 0.5f)
            {
                ItemDescriptionUI.Instance.Show(slot.itemData);
                Hide();
                lastTimeClicked = Time.time; // Update last clicked time
                return; // Prevent double clicks
            }
        });
    }
    
    public void Show(Slot slot, Vector2 position)
    {
        currentSlot = slot;
        menuRoot.SetActive(true);
        menuRoot.transform.position = position;

        deliverButton.onClick.RemoveAllListeners();
        discardButton.onClick.RemoveAllListeners();
        deliverButton.gameObject.SetActive(false);

        if (slot.isEmpty)
        {
            if (Time.time - lastTimeClicked > 0.5f)
            { 
                discardButton.gameObject.SetActive(false);
                descriptionButton.gameObject.SetActive(false);
                lastTimeClicked = Time.time; // Update last clicked time
                return;
            }
        }

        discardButton.gameObject.SetActive(true);
        descriptionButton.gameObject.SetActive(true);
        
        discardButton.onClick.AddListener(() =>
        {
            if (Time.time - lastTimeClicked > 0.5f)
            {
                slot.Clear();
                slot.Update();
                Hide();
                lastTimeClicked = Time.time; // Update last clicked time
                return; // Prevent double clicks
            }
        });

        descriptionButton.onClick.RemoveAllListeners();
        descriptionButton.onClick.AddListener(() =>
        {
            if (Time.time - lastTimeClicked > 0.5f)
            {
                ItemDescriptionUI.Instance.Show(slot.itemData);
                Hide();
                lastTimeClicked = Time.time; // Update last clicked time
                return; // Prevent double clicks
            }
        });
    }
    public void Hide()
    {
        menuRoot.SetActive(false);
    }
}
