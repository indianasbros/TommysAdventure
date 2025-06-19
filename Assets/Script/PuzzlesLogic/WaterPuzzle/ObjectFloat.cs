using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    [SerializeField] private Transform waterLevelReference;
    [SerializeField] private float floatHeightOffset = 0f;
    [SerializeField] private float floatLerpSpeed = 2f;

    private bool isInWater = false;

    void FixedUpdate()
    {
        if (isInWater && waterLevelReference != null)
        {
            float targetY = waterLevelReference.position.y + floatHeightOffset;
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, targetY, Time.fixedDeltaTime * floatLerpSpeed);
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
            isInWater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
            isInWater = false;
    }
}