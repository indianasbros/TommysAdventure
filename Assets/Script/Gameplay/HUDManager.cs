using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public GameObject interactableIcon;
    public GameObject upClose;
    public GameObject upCloseImage;
    void OnEnable()
    {
        InteractSystem.Instance.OnCanInteract += UpdateInteract;
    }

    void OnDisable()
    {
        // Siempre desuscribirse para evitar memory leaks
        InteractSystem.Instance.OnCanInteract -= UpdateInteract;
    }
    void Start()
    {
        interactableIcon.SetActive(false);
        upClose.SetActive(false);
    }
    void UpdateInteract(bool show)
    {
        interactableIcon.SetActive(show);
    }
    public void UpdateUpClose(Sprite image, bool show)
    {
        upCloseImage.GetComponent<Image>().sprite = image;
        upClose.SetActive(show);
    }

}
