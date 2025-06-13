using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class PaintCheck : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onAccessGranted;
    [SerializeField] private UnityEvent onAccessDenied;
    
    [SerializeField] private string notesCombo = "12345";

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
        notesCombo = "12345"; // Set your desired passcode here
    }
    public void AddInput(string input)
    {
        if (accessWasGranted) return;

        if (currentInput != null && currentInput.Length == 5) // 4 max passcode size 
        {
            ClearInput();
            return;
        }
        currentInput += input;
        CheckCombo();
    }
    public void CheckCombo()
    {
        Debug.Log("Current Input: " + currentInput);
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
        accessWasGranted = true;
        onAccessGranted?.Invoke();
        //panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
        audioSource.PlayOneShot(accessGrantedSfx);
    }

}
