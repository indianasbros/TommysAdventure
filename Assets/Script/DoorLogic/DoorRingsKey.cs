using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
public class DoorRingsKey : Doors
{
    public bool haveRing1;
    public bool haveRing2;
    
    protected override void OpenDoor()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canOpen && haveRing1 && haveRing2)
        {
            if (!isOpen)
            {
                targetAngle = (initialAngle - 80f + 360f) % 360f; // abre 80 grados
                isOpen = true;
            }
            else
            {
                targetAngle = initialAngle; // cierra de vuelta
                isOpen = false;
            }
        }
    }
}