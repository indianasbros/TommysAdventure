using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
public class DoorFinalKey : Doors
{
    public bool haveFinalKey;
    
    protected override void OpenDoor()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && canOpen && haveFinalKey)
        {
            if (!isOpen)
            {
                targetAngle = (initialAngle - 80f + 360f) % 360f;
                isOpen = true;
            }
            else
            {
                targetAngle = initialAngle;
                isOpen = false;
            }
        }
    }
  
}