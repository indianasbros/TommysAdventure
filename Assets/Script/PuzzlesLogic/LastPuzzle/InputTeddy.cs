using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTeddy : MonoBehaviour
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
                ExitKey key = hit.collider.GetComponent<ExitKey>();
                if (key != null)
                { 
                    key.canTake = true; // Permite recoger la llave
                }
            }
        }
    }

}
