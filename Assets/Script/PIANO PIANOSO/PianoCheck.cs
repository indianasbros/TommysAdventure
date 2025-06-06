using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class PianoCheck : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onAccessGranted;
    [SerializeField] private UnityEvent onAccessDenied;
    
    [SerializeField] private string notesCombo = "1234";

    public UnityEvent OnAccessGranted => onAccessGranted;
    public UnityEvent OnAccessDenied => onAccessDenied;
    
    [Header("SoundFx")]
    [SerializeField] private AudioClip accessDeniedSfx;
    [SerializeField] private AudioClip accessGrantedSfx;
    [Header("Component References")]
    [SerializeField] private AudioSource audioSource;


    private string currentInput;
    private bool accessWasGranted = false;

    private void Awake()
    {
        ClearInput();
        notesCombo = "1234"; // Set your desired passcode here
    }


    //Gets value from pressedbutton
    public void AddInput(string input, AudioClip noteSfx)
    {
        audioSource.PlayOneShot(noteSfx);
        if (accessWasGranted) return;

        Debug.Log("typo");
        if (currentInput != null && currentInput.Length == 4) // 4 max passcode size 
        {
            ClearInput();
            return;
        }
        currentInput = String.Concat(currentInput, input);
        Debug.Log("prueba: " + currentInput + input);
        CheckCombo();
    }
    public void CheckCombo()
    {
        Debug.Log("Checking Combo: " + currentInput);
        Debug.Log("Correct Combo: " + notesCombo);
        if (currentInput == notesCombo)
        {
            AccessGranted();
            return;
        }
        else
        {
            AccessDenied();
        }
        

    }
    private void AccessDenied()
    {
        onAccessDenied?.Invoke();
        //panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
        audioSource.PlayOneShot(accessDeniedSfx);
    }

    private void ClearInput()
    {
        currentInput = "";
        //keypadDisplayText.text = currentInput;
    }

    private void AccessGranted()
    {
        Debug.Log("Access Granted!");
        accessWasGranted = true;
        onAccessGranted?.Invoke();
        //panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
        audioSource.PlayOneShot(accessGrantedSfx);
    }

}
