using UnityEngine;
using System;

public class TriggerDetector : MonoBehaviour
{
    public event Action<Collider> OnTriggerEntered;
    public event Action<Collider> OnTriggerExited;
    private void OnEnable()
    {
        // Ensure the events are not null before invoking
        if (OnTriggerEntered == null)
            OnTriggerEntered = delegate { };
        if (OnTriggerExited == null)
            OnTriggerExited = delegate { };
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        OnTriggerExited?.Invoke(other);
    }
}