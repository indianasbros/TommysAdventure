using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class KeyCheck : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onAccessGranted;
    public UnityEvent OnAccessGranted => onAccessGranted;
    
    [Header("SoundFx")]
    [SerializeField] private AudioClip accessGrantedSfx;
    [Header("Component References")]
    [SerializeField] private AudioSource audioSource;
    private bool accessWasGranted = false;

    public void AddInput(bool input, AudioClip noteSfx)
    {
        audioSource.PlayOneShot(noteSfx);
        if (accessWasGranted) return;
        if(input)
        {
            // Aquí podrías manejar el caso de acceso concedido si es necesario
            AccessGranted();
            return;
        }
       
    }
    private void AccessGranted()
    {
        accessWasGranted = true;
        onAccessGranted?.Invoke();
        //panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
        audioSource.PlayOneShot(accessGrantedSfx);
    }

}
