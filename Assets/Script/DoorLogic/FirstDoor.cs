using UnityEngine;
using UnityEngine.Audio;
public class FirstDoor : Doors
{

    void OnEnable()
    {
        RoomManager.Instance.OnEnteredFirstPuzzle += OnEnteredFirstPuzzle;
    }
    void OnDisable()
    {
        RoomManager.Instance.OnEnteredFirstPuzzle -= OnEnteredFirstPuzzle;
    }
    void OnEnteredFirstPuzzle()
    {
        puzzleSolved = false;
        speed *= speedMultiplier; // Aumenta la velocidad de apertura
        CloseDoor();
    }
}
    