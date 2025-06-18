using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTouch : MonoBehaviour
{
    
    [Header("Audio")]
    [SerializeField] private AudioClip paintSound;

    [Header("Animación")]
    [SerializeField] private string value;
    private AudioSource audioSource;
    //private Vector3 originalPosition;
    private bool isPressed = false;

    [SerializeField] private PaintCheck check;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //originalPosition = transform.localPosition;

        if (paintSound != null)
        {
            audioSource.clip = paintSound;
            audioSource.playOnAwake = false;
        }
    }

    public void PlayKey()
    {
        if (!isPressed)
        {
            if (paintSound != null)
            {
                audioSource.PlayOneShot(paintSound);
            }
            check.AddInput(value);
            
        }
    }

    

}
