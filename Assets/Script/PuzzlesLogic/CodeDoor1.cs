using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeDoor1 : MonoBehaviour
{

    public Doors doorScript; // referencia al script de la puerta
    public string correctCode = "1234"; // contraseña correcta
    private string enteredCode = "";

    public void AddDigit(string digit)
    {
        enteredCode += digit;
        Debug.Log("Código actual: " + enteredCode);
    }

    public void SubmitCode()
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("¡Código correcto! Puerta desbloqueada.");
            doorScript.CanOpen = true;
        }
        else
        {
            Debug.Log("Código incorrecto. Inténtalo de nuevo.");
            doorScript.CanOpen = false;
        }

        enteredCode = ""; // reinicia para el próximo intento
    }

    public void ClearCode()
    {
        enteredCode = "";
        Debug.Log("Código reiniciado.");
    }
}

