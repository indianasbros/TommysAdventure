
using UnityEngine;

public class PlayerFloat : MonoBehaviour
{
    public bool isFloating = false;
        public bool IsFloating
    {
        get { return isFloating; }
        set
        {
            isFloating = value;
        }
    }
    public Transform waterLevelReference;
    public float floatHeightOffset = 0.5f; // cuánto arriba del agua queremos que esté el personaje
    public float floatLerpSpeed = 5f;      // qué tan suave se ajusta la altura

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

void FixedUpdate()
{
    if (isFloating && waterLevelReference != null)
    {
        float targetY = waterLevelReference.position.y + floatHeightOffset;
        Vector3 position = transform.position;

        // Reemplazamos solo la Y directamente
        position.y = Mathf.Lerp(position.y, targetY, Time.fixedDeltaTime * floatLerpSpeed);

        transform.position = position; // ← sin usar Rigidbody
        rb.velocity = Vector3.zero; // ← para evitar rebotes
    }
}
}