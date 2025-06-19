using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
public class DoorFinalKey : Doors
{
    protected override void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen && PuzzleSolved)
        {
            GameplayManager.Instance.Victory();
        }
    }
  
}