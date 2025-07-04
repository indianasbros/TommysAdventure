using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InputHandler : MonoBehaviour
{
    public Camera mainCamera;
    public GraphicRaycaster uiRaycaster;
    public EventSystem eventSystem;
    private KeyCode interactKey = KeyCode.E;
    public KeyCode InteractKey => interactKey;
    private KeyCode dropKey = KeyCode.Q;
    public KeyCode DropKey => dropKey;
    private KeyCode inventoryKey = KeyCode.I;
    public KeyCode InventoryKey => inventoryKey;
    private static InputHandler _instance;
    public static InputHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("InputHandler");
                _instance = obj.AddComponent<InputHandler>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {

        if (PlayerPrefs.HasKey("Key_0"))
        {
            if (Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
            {
                interactKey = parsedKey;
            }
        }
        //Control Setting for Drop
        if (PlayerPrefs.HasKey("Key_1"))
        {
            if (Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_1"), true, out var parsedKey))
            {
                dropKey = parsedKey;
            }
        }
        //Control Setting for Inventary
        if (PlayerPrefs.HasKey("Key_3"))
        {
            if (Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_3"), true, out var parsedKey))
            {
                inventoryKey = parsedKey;
            }
        }
            
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        /*
        // 👇 NUEVO: chequeo si el mouse está sobre UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Evitamos hacer raycast al mundo
        }*/

        Vector2 mousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue();

        // 👉 Primero chequeamos si hizo click en la UI
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = mousePos;

        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);

        if (results.Count > 0)
        {
            GameObject clickedUI = results[0].gameObject;

            // Intentamos ver si es un SlotUI
            Slot slot = clickedUI.GetComponentInParent<Slot>();

            if (slot != null)
            {
                slot.OnPointerClick();
                return;
            }
            return;
        }

        // 👉 Si no fue UI, chequeamos si fue un objeto 2D
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            Debug.Log("2D World Clicked on: " + hit.collider.name);
            // Lógica para objetos del mundo
        }
    }
}
