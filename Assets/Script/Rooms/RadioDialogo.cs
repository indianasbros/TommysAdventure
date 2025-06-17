using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadioDialogo : MonoBehaviour
{
    [SerializeField] private AudioClip audioDialogo;
    private AudioSource audioSource;
    private bool playerInRank = false;
    private bool playing = false;

    private static List<RadioDialogo> radios = new List<RadioDialogo>();
    private KeyCode interactKey = KeyCode.E;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioDialogo;
        radios.Add(this);
    }

    void OnDestroy()
    {
        radios.Remove(this);
    }

    void Update()
    {
        if (playerInRank && Input.GetKeyDown(interactKey) && !playing)
        {
            StartCoroutine(PlayingDialog());
        }
    }

    private IEnumerator PlayingDialog()
    {
        foreach (RadioDialogo radio in radios)
        {
            if (radio != this)
            {
                radio.StopAudio();
            }
        }

        playing = true;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        playing = false;
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            playing = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRank = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRank = false;
        }
    }
}
