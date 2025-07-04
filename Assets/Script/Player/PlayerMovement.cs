using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [Header("-----Movement Settings-----")]
    [SerializeField] private const float baseSpeed = 10f;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSmoothTime = 0.12f;
    private float BuffedSpeed = 20f;
    private bool freezeMovement;
    public bool FreezeMovement
    {
        get => freezeMovement;
        set
        {
            freezeMovement = value;
            if (freezeMovement)
            {
                stepAudioSource.Stop();
            }
            else if (!isSwimming && !stepAudioSource.isPlaying)
            {
                stepAudioSource.Play();
            }
        }
    }

    [Header("-----Power-Up Settings-----")]
    [SerializeField] private ItemData powerUpSpeed;
    [Header("-----Swimming State-----")]
    [SerializeField] private PlayerFloat playerFloat;
    private bool isSwimming;

    [Header("-----Camera Settings-----")]
    public float mouseSensitivity = 2f;
    public float minVerticalAngle = -40f;
    public float maxVerticalAngle = 40f;
    private float jumpForce = 10f;
    [SerializeField] private bool onFloor;
    private Rigidbody rigidbody3D;
    private Vector2 lookInput;
    private Animator animator;
    Vector3 camRight = Vector3.zero;
    Vector3 camForward = Vector3.zero;
    [SerializeField] private Camera mainCamera;

    [Header("-----Audio Settings-----")]
    [SerializeField] private AudioSource stepAudioSource;
    [SerializeField] private AudioClip stepClip;
    [SerializeField] private AudioMixerGroup sfxGroup;
    private Vector3 playerInput;
    private Vector3 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody3D = GetComponent<Rigidbody>();
        speed = baseSpeed;
        // Seguridad para físicas
        rigidbody3D.interpolation = RigidbodyInterpolation.Interpolate;
        rigidbody3D.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Audio
        stepAudioSource.clip = stepClip;
        stepAudioSource.loop = true;
        stepAudioSource.outputAudioMixerGroup = sfxGroup;
        if(TryGetComponent<AudioListener>(out var audioListener))
        {
            audioListener.enabled = true;
        }
        else
        {
            gameObject.AddComponent<AudioListener>();
            Debug.LogWarning("AudioListener was not found on PlayerMovement. Added a new one.");
        }
    }

     void Update()
    {
        if (FreezeMovement) return;

        isSwimming = playerFloat != null && playerFloat.IsFloating;

        lookInput.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        lookInput.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        lookInput.y = Mathf.Clamp(lookInput.y, minVerticalAngle, maxVerticalAngle);

        Jump();
        ProcessInput();
        HandleAnimationAndAudio();
    }
    void FixedUpdate()
    {
        if (FreezeMovement)
        {
            if (animator.speed > 0f)
            {
                animator.StopPlayback();
            }
            return;
        }
        UpdateSpeed();
        MovePlayerWithPhysics();
    }

    void UpdateSpeed()
    {
        if (PowerUps.Instancia.HasPowerUp(powerUpSpeed) && speed != BuffedSpeed)
        {
            speed = BuffedSpeed;
            
        }
        else if (!PowerUps.Instancia.HasPowerUp(powerUpSpeed) && speed != baseSpeed)
        {
            speed = BuffedSpeed;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            animator.SetTrigger("Jump");
            rigidbody3D.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ProcessInput()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        moveDirection = playerInput.x * camRight + playerInput.z * camForward;
    }

    void MovePlayerWithPhysics()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 move = moveDirection.normalized * speed * Time.fixedDeltaTime;
            Vector3 targetPosition = rigidbody3D.position + move;
            rigidbody3D.MovePosition(targetPosition);

            // Rotación hacia dirección de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rigidbody3D.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime));
        }
    }

    //Funcion para la direccion de la camara.
    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    void HandleAnimationAndAudio()
    {
        if (isSwimming)
        {
            animator.SetBool("isSwimming", true);
            animator.SetFloat("SwimSpeed", playerInput.magnitude);
        }
        else
        {
            animator.SetBool("isSwimming", false);
            animator.SetFloat("Velocity", playerInput.magnitude);
        }

        if (playerInput.magnitude >= 0.1f)
        {
            if (!stepAudioSource.isPlaying && !isSwimming)
            {
                stepAudioSource.Play();
            }
        }
        else
        {
            if (stepAudioSource.isPlaying)
            {
                stepAudioSource.Stop();
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }
}