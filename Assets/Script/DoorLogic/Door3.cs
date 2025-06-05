using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : Doors
{
    public bool puzzle3;
    
    protected override void OpenDoor()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && canOpen && puzzle3)
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
