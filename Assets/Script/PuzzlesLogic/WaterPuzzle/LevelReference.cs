using UnityEngine;

public class WaterSurfaceTracker : MonoBehaviour
{
    [SerializeField] private Transform waterSurface;
    [SerializeField] private float baseHeight = 0f; 
    [SerializeField] private float initialScaleY = 1f;
    void Start()
    {

        initialScaleY = transform.localScale.y;
        baseHeight = transform.position.y;
    }

    void Update()
    {
        if (waterSurface != null)
        {
            float currentScaleY = transform.localScale.y;
            float surfaceY = baseHeight + (currentScaleY * 0.5f); 
            Vector3 pos = waterSurface.position;
            pos.y = surfaceY;
            waterSurface.position = pos;
        }
    }
}
