using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : Doors
{
    private bool puzzle3;
    public bool Puzzle3Solved
    {
        get { return puzzle3; }
        set
        {
            puzzle3 = value;
        }
    }
    
    protected override void OpenDoor()
    {

        if (Input.GetKeyDown(KeyCode.E) && canOpen && puzzle3)
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
