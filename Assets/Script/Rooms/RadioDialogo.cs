using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RadioDialogo : MonoBehaviour
{
    [SerializeField] private AudioClip audioDialogo;
    private AudioSource audioSource;
    private bool playerInRank = false;
    private bool playing = false;
    public bool Playing { get { return playing; } private set { playing = value; } }
    private KeyCode interactKey = KeyCode.E;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioDialogo;
        DialogueManager.Instance.radios.Add(this);
    }

    void OnDestroy()
    {
        DialogueManager.Instance.radios.Remove(this);
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
        List<RadioDialogo> radios = DialogueManager.Instance.radios;
        foreach (RadioDialogo radio in radios)
        {
            if (radio != this)
            {
                radio.StopAudio();
            }
        }

        Playing = true;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Playing = false;
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            Debug.Log("paro audio");
            audioSource.Stop();
            Playing = false;
        }
    }
    public void PauseAudio()
    {
        audioSource.Pause();
    }
    public void ResumeAudio()
    {
        audioSource.Play();   
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
