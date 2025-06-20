using UnityEngine;

public class WaterSurfaceTracker : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform waterSurface;

    [Header("Ajuste fino de altura visual")]
    [Tooltip("Resta este valor para alinear el objeto vacío con la superficie real del agua (ej: 0.25f si queda muy alto)")]
    [SerializeField] private float ajusteVisual = 0f;

    private float baseHeight;

    void Start()
    {
        baseHeight = transform.position.y;
    }

    void Update()
    {
        float currentScaleY = transform.localScale.y;
        float surfaceY = baseHeight + (currentScaleY * 0.5f) - ajusteVisual;

        if (waterSurface != null)
        {
            waterSurface.position = new Vector3(
                waterSurface.position.x,
                surfaceY,
                waterSurface.position.z
            );
        }
    }
}
