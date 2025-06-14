using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canInteract;
    public bool isInteracting;
    public GameObject cameraToChangeTo;
    private KeyCode interactKey = KeyCode.E;

    void Start()
    {
        canInteract = true;
        isInteracting = false;

        if (PlayerPrefs.HasKey("Key_4"))
        {
            string savedKey = PlayerPrefs.GetString("Key_4");
            Debug.Log("Saved key from PlayerPrefs: " + savedKey);

            if (System.Enum.TryParse<KeyCode>(savedKey, true, out var parsedKey))
            {
                interactKey = parsedKey;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInteracting)
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
