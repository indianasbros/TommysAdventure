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
        CinemachineController.Instance.ChangeCamera(cameraToChangeTo.name);

        isInteracting = true;
        canInteract = false;
    }
    public void ChangeToMainCamera()
    {
        CinemachineController.Instance.ChangeMainCamera();
        isInteracting = false;
        canInteract = true;
    }
}
