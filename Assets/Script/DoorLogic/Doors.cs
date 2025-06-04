using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
public class Doors : MonoBehaviour
{
    public float speed = 90f; // grados por segundo
    public Axis rotationAxis = Axis.Y; // eje por defecto (cámbialo en el Inspector)
    private bool canOpen;
    public bool CanOpen
    {
        get { return canOpen; }
        set { canOpen = value; }
    }
    private float initialAngle;
    private float targetAngle;
    private bool isOpen = false;

    public enum Axis { X, Y, Z }

    void Start()
    {
        initialAngle = GetCurrentAngle();
        targetAngle = initialAngle;
    }

    void Update()
    {
        float currentAngle = GetCurrentAngle();

        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.1f)
        {
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, speed * Time.deltaTime);
            SetCurrentAngle(newAngle);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canOpen)
        {
            if (!isOpen)
            {
                targetAngle = (initialAngle - 80f + 360f) % 360f; // abre 80 grados
                isOpen = true;
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