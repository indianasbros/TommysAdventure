using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDoor : Doors
{
    private bool waterPuzzleSolved;
    public bool WaterPuzzleSolved
    {
        get { return waterPuzzleSolved; }
        set
        {
            waterPuzzleSolved = value;
        }
    }
    protected override void OpenDoor()
    {

        if (Input.GetKeyDown(KeyCode.E) && canOpen && waterPuzzleSolved)
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
