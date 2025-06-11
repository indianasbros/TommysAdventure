using UnityEngine;

public class FloatSensor : MonoBehaviour
{
    public PlayerFloat playerFloat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            playerFloat.IsFloating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            playerFloat.IsFloating = false;
        }
    }
}
