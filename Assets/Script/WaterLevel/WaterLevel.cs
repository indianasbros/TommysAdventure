using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] float waterIncrese = 5f;
    [SerializeField] float waterMax = 100f;

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        if (scale.y < waterMax)
        {
            scale.y += waterIncrese * Time.deltaTime;
            transform.localScale = scale;
        }
    }
    
}
