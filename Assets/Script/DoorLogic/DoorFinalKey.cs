using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
public class DoorFinalKey : Doors
{
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
        if (Input.GetKeyDown(interactKey) && canOpen)
        {
            GameplayManager.Instance.Victory();
        }
    }
  
}