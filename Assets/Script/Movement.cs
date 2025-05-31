using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int life = 10;
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject gameObject;
    public Rigidbody rigidbody3D;
    [SerializeField] private float rotationPower = 3f; // Velocidad de rotación de la cámara
    [SerializeField] private Transform followTransform; // Referencia al objeto que sigue la cámara (como un pivot en el personaje)

    private Vector2 _look; // Entrada del mouse para rotación
    private Vector2 _move; // Entrada de movimiento WASD
    private Quaternion nextRotation;
    private Vector3 nextPosition;
    private float rotationLerp = 10f; // Suavizado de rotación
    private int aimValue = 0; // Puedes ignorar o definirlo dependiendo de tu lógica de apuntado
    void Start()
    {
        gameObject = GetComponent<GameObject>();
        rigidbody3D = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Movement();
        CameraRotation();
    }

    void Movement()
    {
        float speedX = Input.GetAxis("Horizontal");
        float speedZ = Input.GetAxis("Vertical");

        if (life > 0)
        {
            Vector3 moveDirection = new Vector3(speedX, 0, speedZ).normalized;
            Vector3 velocity = moveDirection * speed;
            Vector3 newPosition = rigidbody3D.position + velocity * Time.deltaTime;
            rigidbody3D.MovePosition(newPosition);
        }
        else
        {
            //muere
        }
    }

    void CameraRotation()
    {

        #region Follow Transform Rotation
        transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
        #endregion
        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);
        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;
        var angle = followTransform.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;
        #endregion

        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if (_move.x == 0 && _move.y == 0)
        {
            nextPosition = transform.position;

            if (aimValue == 1)
            {
                transform.rotation = Quaternion.Euler(0, followTransform.rotation.eulerAngles.y, 0);
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }
            return;
        }
        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
        nextPosition = transform.position + position;

        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

}
