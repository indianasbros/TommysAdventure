using NavKeypad;
using UnityEngine;

public class ButtonRaycaster : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                KeypadButton button = hit.collider.GetComponent<KeypadButton>();
                if (button != null)
                {
                    Debug.Log("Button pressed: " + button?.name);
                    button.PressButton();
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 10f); // Dibuja la línea del rayo
        Gizmos.DrawSphere(ray.origin, 0.1f); // Dibuja una esfera en el origen del rayo
    }
}