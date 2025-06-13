using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] float waterIncrese = 5f;
    [SerializeField] float waterMax = 100f;
    [SerializeField] float waterMin = 0f;
    [SerializeField] public bool objectTaked;
    [SerializeField] WaterDoor waterDoor;

    public bool ObjectTaked
    {
        get { return objectTaked; }
        set
        {
            objectTaked = value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        if (scale.y < waterMax && objectTaked == false)
        {
            scale.y += waterIncrese * Time.deltaTime;
            transform.localScale = scale;
        }
        else if (objectTaked)
        {
            if (scale.y > waterMin)
            {
                scale.y -= waterIncrese * Time.deltaTime;
                transform.localScale = scale;
            }
        }

        if (scale.y == waterMin)
        {
            waterDoor.WaterPuzzleSolved = true;
        }
    }
    
}
