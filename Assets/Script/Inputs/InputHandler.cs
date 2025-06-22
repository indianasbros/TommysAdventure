using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    public Camera mainCamera;
    public GraphicRaycaster uiRaycaster;
    public EventSystem eventSystem;

    void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
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
