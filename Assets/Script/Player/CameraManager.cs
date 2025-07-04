using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineFreeLook mainGameplayCam;
    public CinemachineBrain brain;
    public static CameraManager Instance { get; private set; }

    [Header("Main Camera")]
    public Camera mainCamera;


    [Header("Cinemachine Camera")]
    [Tooltip("Animator component controlling the camera transitions.")]
    [SerializeField] Animator anim;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }
        Instance = this;

    }

    void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.m_CameraActivatedEvent.AddListener(OnCameraSwitched);
        mainGameplayCam.Priority = 100; // fuerza que esta sea la primera
        mainCamera.GetComponent<AudioListener>().enabled = false; // Asegura que el AudioListener no esté activo al inicio
    }
    public void ChangeCamera(string toCamera)
    {
        if (toCamera != null)
        {
            anim.Play(toCamera);
        }
        else
        {
            Debug.LogWarning("Camera to change to is null.");
        }
    }
    public void ChangeMainCamera()
    {
        anim.Play(mainGameplayCam.name);
    }
    public void ActivateAudioListener()
    {
        if (mainCamera != null)
        {
            mainCamera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            Debug.LogWarning("Main Camera is not assigned.");
        }
    }
    public void DeactivateAudioListener()
    {
        if (mainCamera != null)
        {
            mainCamera.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            Debug.LogWarning("Main Camera is not assigned.");
        }
    }
    private void OnCameraSwitched(ICinemachineCamera fromCam, ICinemachineCamera toCam)
    {
        if ((object)toCam == (object)mainGameplayCam)
        {
            LockCursor(false);
        }
        else
        {
            LockCursor(true);
        }
    }
    public void LockCursor(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}