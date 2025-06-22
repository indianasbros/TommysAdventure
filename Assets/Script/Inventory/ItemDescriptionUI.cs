using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;

    public static ItemDescriptionUI Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(ItemData item)
    {
        nameText.text = item.itemName;
        descriptionText.text = item.description;
        if (iconImage != null) iconImage.sprite = item.icon;

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
