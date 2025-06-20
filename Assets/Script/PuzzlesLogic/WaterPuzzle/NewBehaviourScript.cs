using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurfa : MonoBehaviour
{
    [SerializeField] private Transform waterSurface;

    private float baseHeight;

    void Start()
    {
        baseHeight = transform.position.y;
    }

    void Update()
    {
        float currentScaleY = transform.localScale.y;
        float surfaceY = baseHeight + (currentScaleY * 0.5f);
        waterSurface.position = new Vector3(waterSurface.position.x, surfaceY, waterSurface.position.z);
    }
}
