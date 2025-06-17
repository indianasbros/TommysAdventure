using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerMovement : MonoBehaviour
{
    [Header("-----Movement Settings-----")]
    public float speed = 6f;
    public float rotationSmoothTime = 0.12f;

    [Header("-----Camera Settings-----")]
    //public Transform followTransform; // El pivot o la cámara que sigue al personaje
    public float mouseSensitivity = 2f;
    public float minVerticalAngle = -40f;
    public float maxVerticalAngle = 40f;
    float jumpForce = 10f;
    bool onFloor;
    private Rigidbody rigidbody3D;
    private float turnSmoothVelocity;
    private Vector2 lookInput; // Para guardar el movimiento del mouse
    Quaternion rotation;
    Animator animator;

    // Steps Audio
    [Header("-----Audio Settings-----")]
    [SerializeField] private AudioSource stepAudioSource;
    [SerializeField] private AudioClip stepClip;
    [SerializeField] private AudioMixerGroup sfxGroup;

    void Start()
    {
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
        // Capturar input del mouse para rotación de cámara
        lookInput.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        lookInput.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        lookInput.y = Mathf.Clamp(lookInput.y, minVerticalAngle, maxVerticalAngle);

        Jump();
        MoveAndRotate();
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
        animator.SetFloat("Velocity", inputDirection.magnitude);
        if (inputDirection.magnitude >= 0.1f)
        {
            // Steps Audio
            if (!stepAudioSource.isPlaying)
            {
                stepAudioSource.Play();
            }

            // Aquí NO calculamos el ángulo basado en la cámara
            // Solo movemos relativo al personaje

            
            // Movimiento relativo al personaje
            Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;

            Vector3 newPos = rigidbody3D.position + moveDir.normalized * speed * Time.deltaTime;
            rigidbody3D.MovePosition(newPos);

            // Opcional: si quieres que el personaje rote hacia donde se mueve
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            //Steps Audio
            if (stepAudioSource.isPlaying)
            {
                stepAudioSource.Stop();
            }
        }
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }

    }
    protected void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }
}
