using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class PianoCheck : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onAccessGranted;
    [SerializeField] private UnityEvent onAccessDenied;
    
    [SerializeField] private int notesCombo = 1234;

    public UnityEvent OnAccessGranted => onAccessGranted;
    public UnityEvent OnAccessDenied => onAccessDenied;
    
    [Header("SoundFx")]
    [SerializeField] private AudioClip accessDeniedSfx;
    [SerializeField] private AudioClip accessGrantedSfx;
    [Header("Component References")]
    [SerializeField] private AudioSource audioSource;


    private string currentInput;
    private bool displayingResult = false;
    private bool accessWasGranted = false;

    private void Awake()
    {
        ClearInput();
    }


    //Gets value from pressedbutton
    public void AddInput(string input, AudioClip noteSfx)
    {
        audioSource.PlayOneShot(noteSfx);
        if (displayingResult || accessWasGranted) return;
        switch (input)
        {
            case "enter":
                CheckCombo();
                break;
            default:
                if (currentInput != null && currentInput.Length == 4) // 4 max passcode size 
                {
                    return;
                }
                currentInput += input;
                
                break;
        }

    }
    public void CheckCombo()
    {
        if (int.TryParse(currentInput, out var currentKombo))
        {
            if (currentKombo == notesCombo)
            {
                AccessGranted();
                ClearInput();
                return;
            }
            AccessDenied();
            ClearInput();

        }
        else
        {
            Debug.LogWarning("Couldn't process input for some reason..");
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
