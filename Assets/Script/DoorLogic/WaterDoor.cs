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

    private KeyCode interactKey = KeyCode.E;

    void Start()
    {
        //Control Setting for Interact
        if (PlayerPrefs.HasKey("Key_0"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
            {
                interactKey = parsedKey;
            }
        }
    }


    protected override void OpenDoor()
    {

        if (Input.GetKeyDown(interactKey) && canOpen && waterPuzzleSolved)
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
