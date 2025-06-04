using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canInteract;
    public bool isInteracting;
    public GameObject cameraToChangeTo;
    void Start()
    {
        canInteract = true;
        isInteracting = false;
    }
    public void ChangeToCamera()
    {
        CinemachineController.Instance.ChangeCamera(cameraToChangeTo.name);
        isInteracting = true;
        canInteract = false;
    }
}
