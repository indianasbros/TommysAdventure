using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touching : MonoBehaviour
{
    
    [Header("Audio")]
    [SerializeField] private AudioClip keySound;

    [Header("Animación")]
    [SerializeField] private float pressDepth = 0.3f;
    [SerializeField] private float pressSpeed = 1f;
    [SerializeField] private string value;
    private AudioSource audioSource;
    private Vector3 originalPosition;
    private bool isPressed = false;

    [SerializeField] private PianoCheck pianoCheck;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalPosition = transform.localPosition;

        if (keySound != null)
        {
            audioSource.clip = keySound;
            audioSource.playOnAwake = false;
        }
    }

    public void PlayKey()
    {
        if (!isPressed)
        {
            if (keySound != null)
            {
                audioSource.PlayOneShot(keySound);
            }
            pianoCheck.AddInput(value, keySound);
            StartCoroutine(AnimateKeyPress());
        }
    }

    private IEnumerator AnimateKeyPress()
    {
        isPressed = true;

        Vector3 targetPosition = originalPosition - new Vector3(0, pressDepth, 0);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / pressSpeed;
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / pressSpeed;
            transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, t);
            yield return null;
        }

        transform.localPosition = originalPosition;
        isPressed = false;
    }

}
