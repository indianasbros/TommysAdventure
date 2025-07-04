using UnityEngine;

public class Interactable : MonoBehaviour, ICameraInteractable
{
    private bool canInteract;
    public bool CanInteract
    {
        get { return canInteract; }
        set { canInteract = value; }

    }
    public bool isInteracting;
    public GameObject cameraToChangeTo;

    void Start()
    {
        canInteract = true;
        isInteracting = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(InputHandler.Instance.InteractKey) && isInteracting)
        {
            ChangeToMainCamera();
        }
    }
    public void ChangeToCamera()
    {
        CameraManager.Instance.ChangeCamera(cameraToChangeTo.name);

        isInteracting = true;
        canInteract = false;
    }
    public void ChangeToMainCamera()
    {
        CameraManager.Instance.ChangeMainCamera();
        isInteracting = false;
        canInteract = true;

    }
}
