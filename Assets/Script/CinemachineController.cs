using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    public static CinemachineController Instance { get; private set; }

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
        // Opcional: No destruir al cambiar de escena
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public void ChangeCamera(string cameraToChangeTo)
    {
        if (cameraToChangeTo != null)
        {
            
            anim.Play(cameraToChangeTo);
        }
        else
        {
            Debug.LogWarning("Camera to change to is null.");
        }
    }
    public void ChangeMainCamera()
    {
        anim.Play("VCam_Gameplay");
    }
}
