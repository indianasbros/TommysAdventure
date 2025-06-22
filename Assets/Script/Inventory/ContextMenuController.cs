using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    public GameObject menuRoot;
    public Button deliverButton;
    public Button discardButton;
    public Button descriptionButton;
    private Slot currentSlot;

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

    public void ShowWithDeliver(Slot slot, Vector2 position)
    {
        currentSlot = slot;
        menuRoot.SetActive(true);
        menuRoot.transform.position = position;

        deliverButton.onClick.RemoveAllListeners();
        discardButton.onClick.RemoveAllListeners();

        deliverButton.onClick.AddListener(() =>
        {
            ObjectInventorySystem.Instance.TryAddItem(currentSlot.itemData, currentSlot.quantity);
            currentSlot.Clear();
            currentSlot.Update();
            Hide();
        });

        discardButton.onClick.AddListener(() =>
        {
            currentSlot.Clear();
            currentSlot.Update();
            Hide();
        });
        descriptionButton.onClick.RemoveAllListeners();
        descriptionButton.onClick.AddListener(() =>
        {
            ItemDescriptionUI.Instance.Show(slot.itemData);
            Hide();
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
            discardButton.gameObject.SetActive(false);
            descriptionButton.gameObject.SetActive(false);
            return;
        }

        discardButton.gameObject.SetActive(true);
        descriptionButton.gameObject.SetActive(true);
        
        discardButton.onClick.AddListener(() =>
        {
            slot.Clear();
            slot.Update();
            Hide();
        });

        descriptionButton.onClick.RemoveAllListeners();
        descriptionButton.onClick.AddListener(() =>
        {
            ItemDescriptionUI.Instance.Show(slot.itemData);
            Hide();
        });
    }
    public void Hide()
    {
        menuRoot.SetActive(false);
    }
}
