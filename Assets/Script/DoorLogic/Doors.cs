using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
public class Doors : MonoBehaviour
{
    [SerializeField] private bool puzzleSolved;
    private KeyCode interactKey = KeyCode.E;

    public bool PuzzleSolved
    {
        get { return puzzleSolved; }
        set
        {
            puzzleSolved = value;
        }
    }
    protected float speed = 30f; // grados por segundo
    protected Axis rotationAxis = Axis.Y; // eje por defecto (cámbialo en el Inspector)
    protected float initialAngle;
    protected float targetAngle;
    protected bool isOpen = false;
    [SerializeField]protected bool canOpen;
    public bool CanOpen
    {
        get { return canOpen; }
        set { canOpen = value; }
    }

    protected enum Axis { X, Y, Z }

    [Header("-----Audio Settings-----")]
    [SerializeField] public AudioClip doorSound;
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
        if (PlayerPrefs.HasKey("Key_4"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_4"), true, out var parsedKey))
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
        Debug.LogWarning(interactKey);
        Debug.LogWarning(puzzleSolved);
        Debug.LogWarning(canOpen);
        if (Input.GetKeyDown(interactKey) && canOpen && puzzleSolved)
        {
            if (!isOpen)
            {
                targetAngle = (initialAngle - 80f + 360f) % 360f; // abre 80 grados
                isOpen = true;

                //Door Audio
                if (doorSound != null)
                {
                    audioSource.PlayOneShot(doorSound);
                }
            }
            else
            {
                targetAngle = initialAngle; // cierra de vuelta
                isOpen = false;
            }
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
            // Ya no cerramos automáticamente al salir
        }
    }
}