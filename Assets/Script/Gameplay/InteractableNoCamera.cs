using UnityEngine;

public class InteractableNoCamera : MonoBehaviour
{
    protected bool canInteract;
    void Start()
    {
        canInteract = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
