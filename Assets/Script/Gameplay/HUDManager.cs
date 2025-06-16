using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
    void UpdateInteract(bool show)
    {
        interactableIcon.SetActive(show);
    }

}
