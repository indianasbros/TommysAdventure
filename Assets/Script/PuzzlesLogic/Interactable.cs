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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInteracting)
        {
            Debug.Log("Changing to main camera");
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
