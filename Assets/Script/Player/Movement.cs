using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [Header("-----Movement Settings-----")]
    [SerializeField] public float speed;
    public float rotationSmoothTime = 0.12f;
    private float BuffedSpeed = 20f;

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
    private float turnSmoothVelocity;
    private Vector2 lookInput;
    private Animator animator;

    [Header("-----Audio Settings-----")]
    [SerializeField] private AudioSource stepAudioSource;
    [SerializeField] private AudioClip stepClip;
    [SerializeField] private AudioMixerGroup sfxGroup;

    void Start()
    {
        speed = 10f;
        animator = GetComponent<Animator>();
        rigidbody3D = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Steps Audio
        stepAudioSource.clip = stepClip;
        stepAudioSource.loop = true;
        stepAudioSource.outputAudioMixerGroup = sfxGroup;
    }

    void Update()
    {
        // Actualizar estado de nado desde el script PlayerFloat
        isSwimming = playerFloat != null && playerFloat.IsFloating;

        // Capturar input del mouse para rotación de cámara
        lookInput.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        lookInput.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        lookInput.y = Mathf.Clamp(lookInput.y, minVerticalAngle, maxVerticalAngle);

        UpdateSpeed();
        Jump();
        MoveAndRotate();
    }

    void UpdateSpeed()
    {
        if (PowerUps.Instancia.PowerUpSpeed)
        {
            speed = BuffedSpeed;
            PowerUps.Instancia.PowerUpSpeed = false;
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

    void MoveAndRotate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Animaciones según estado
        if (isSwimming)
        {
            animator.SetBool("isSwimming", true);
            animator.SetFloat("SwimSpeed", inputDirection.magnitude);
        }
        else
        {
            animator.SetBool("isSwimming", false);
            animator.SetFloat("SwimSpeed", 0);
            animator.SetFloat("Velocity", inputDirection.magnitude);
        }

        if (inputDirection.magnitude >= 0.1f)
        {
            // Audio
            if (!stepAudioSource.isPlaying && !isSwimming)
            {
                stepAudioSource.Play();
            }

            Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;
            Vector3 newPos = rigidbody3D.position + moveDir.normalized * speed * Time.deltaTime;
            rigidbody3D.MovePosition(newPos);

            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
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