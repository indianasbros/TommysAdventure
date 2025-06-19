using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] float waterIncrese = 5f;
    [SerializeField] float waterMax = 90f;
    [SerializeField] float waterMin = 0f;
    bool canIncrese = false;
    [SerializeField] public RoomArea roomTrigger;
    [Header("----Puertas----")]
    [SerializeField] public Doors waterDoor;
    [SerializeField] public Doors previousDoor;

    void Start()
    {
        canIncrese = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (roomTrigger.PlayerInRoom)
        {
            previousDoor.CloseDoor();
        }
        if (previousDoor.IsOpen && waterDoor.IsOpen && waterDoor.PuzzleSolved)
        {
            canIncrese = false;
        }
        if (!waterDoor.PuzzleSolved && !previousDoor.IsOpen && roomTrigger.PlayerInRoom)
        {
            canIncrese = true;
        }

        Vector3 scale = transform.localScale;
        if (canIncrese)
        {
            if (scale.y < waterMax)
            {
                scale.y += waterIncrese * Time.deltaTime;
                transform.localScale = scale;
            }

            if (scale.y == waterMin)
            {
                waterDoor.PuzzleSolved = true;
            }
        }
        else if (waterDoor.PuzzleSolved)
        {
            if (scale.y > waterMin)
            {
                scale.y -= waterIncrese * Time.deltaTime;
                transform.localScale = scale;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roof"))
        {
            canIncrese = false;
        }
    }

}
