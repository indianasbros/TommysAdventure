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

    private KeyCode interactKey = KeyCode.E;


    void Start()
    {
        canInteract = true;
        isInteracting = false;

        //Control Setting for Interact
        if (PlayerPrefs.HasKey("Key_0"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
            {
                interactKey = parsedKey;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInteracting)
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
