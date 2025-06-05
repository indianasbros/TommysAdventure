using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : Doors
{
    public bool puzzle2;
    public bool Puzzle2Solved
    {
        get { return puzzle2; }
        set
        {
            puzzle2 = value;
        }
    }
    
    protected override void OpenDoor()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canOpen && puzzle2)
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
