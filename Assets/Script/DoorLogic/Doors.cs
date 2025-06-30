using UnityEngine;
using UnityEngine.Audio;
public class Doors : MonoBehaviour
{
    [SerializeField] private bool puzzleSolved;
    protected KeyCode interactKey = KeyCode.E;

    public bool PuzzleSolved
    {
        get { return puzzleSolved; }
        set
        {
            puzzleSolved = value;
        }
    }
    protected float speed = 60f; // grados por segundo
    protected int speedMultiplier = 3;
    protected Axis rotationAxis = Axis.Y; // eje por defecto (cámbialo en el Inspector)
    protected float initialAngle;
    protected float targetAngle;
    protected bool isOpen = false;
    public bool IsOpen { get; set; }
    private bool isFinalDoor = false;
    public bool IsFinalDoor
    {
        get { return isFinalDoor; }
        set { isFinalDoor = value; }
    }
    [SerializeField] protected bool canOpen;
    public bool CanOpen
    {
        get { return canOpen; }
        set { canOpen = value; }
    }

    protected enum Axis { X, Y, Z }

    [Header("-----Audio Settings-----")]
    [SerializeField] public AudioClip doorOpenSound;
    [SerializeField] public AudioClip doorCloseSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup sfxGroup;

    void Start()
    {
        canOpen = false;
        initialAngle = GetCurrentAngle();
        targetAngle = initialAngle;

        //Door Audio
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxGroup;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

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
        float currentAngle = GetCurrentAngle();

        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.1f)
        {
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, speed * Time.deltaTime);
            SetCurrentAngle(newAngle);
        }
        OpenDoor();
    }

    virtual protected void OpenDoor()
    {
        if (Input.GetKeyDown(interactKey) && canOpen && puzzleSolved)
        {
            if (isFinalDoor)
            {
                GameplayManager.Instance.Victory();
            }
            if (!isOpen)
                {
                    targetAngle = (initialAngle - 80f + 360f) % 360f; // abre 80 grados
                    isOpen = true;

                    //Door Audio
                    if (doorOpenSound != null)
                    {
                        audioSource.PlayOneShot(doorOpenSound);
                    }
                }
                else
                {
                    if (doorCloseSound != null)
                    {
                        audioSource.PlayOneShot(doorCloseSound);
                    }
                    targetAngle = initialAngle; // cierra de vuelta
                    isOpen = false;
                }
        }
    }
    
    public void CloseDoor()
    {
        if (isOpen && puzzleSolved)
        {
            targetAngle = initialAngle; // cierra de vuelta
            //Door Audio
            if (doorCloseSound != null)
            {
                audioSource.PlayOneShot(doorCloseSound);
            }
            isOpen = false;
        }

    }
    float GetCurrentAngle()
    {
        switch (rotationAxis)
        {
            case Axis.X: return transform.eulerAngles.x;
            case Axis.Y: return transform.eulerAngles.y;
            case Axis.Z: return transform.eulerAngles.z;
            default: return 0f;
        }
    }

    void SetCurrentAngle(float angle)
    {
        Vector3 euler = transform.eulerAngles;
        switch (rotationAxis)
        {
            case Axis.X: euler.x = angle; break;
            case Axis.Y: euler.y = angle; break;
            case Axis.Z: euler.z = angle; break;
        }
        transform.eulerAngles = euler;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;

        }
        
    }
}