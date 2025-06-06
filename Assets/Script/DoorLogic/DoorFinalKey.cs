using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class DoorFinalKey : Doors
{

    private bool haveFinalKey;
    public bool HaveFinalKey
    {
        get { return haveFinalKey; }
        set
        {
            haveFinalKey = value;
        }
    }
    [SerializeField] CursorManager cursorManager;
    protected override void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen && haveFinalKey)
        {
            Destroy(CinemachineController.Instance.gameObject); // Destruye el objeto CinemachineController
            Destroy(GameplayManager.Instance.gameObject); // Destruye el objeto GameplayManager
            Destroy(InventorySystem.Instance.gameObject); // Destruye el objeto InventorySystem
            Destroy(TimeController.Instance.gameObject); // Destruye el objeto TimeController
            
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
            return; // Salimos del método si se abre la escena de victoria
            /*if (!isOpen)
            {
                targetAngle = (initialAngle - 80f + 360f) % 360f; // abre 80 grados
                isOpen = true;
            }
            else
            {
                targetAngle = initialAngle; // cierra de vuelta
                isOpen = false;
            }*/
        }
    }
  
}