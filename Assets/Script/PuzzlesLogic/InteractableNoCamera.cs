using UnityEngine;

public class InteractableNoCamera : MonoBehaviour
{
    private bool canInteract;
    void Start()
    {
        canInteract = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            
        }
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
