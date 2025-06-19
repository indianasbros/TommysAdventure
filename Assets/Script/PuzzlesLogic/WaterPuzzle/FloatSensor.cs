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
        if (other.CompareTag("Roof"))
        {
            playerFloat.IsFloating = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            playerFloat.IsFloating = false;
        }
        if (other.CompareTag("Roof"))
        {
            playerFloat.IsFloating = true;
        }
    }
}
