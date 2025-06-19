using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public List<RadioDialogo> radios = new();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void PauseAudio()
    {
        foreach (RadioDialogo radio in radios)
        {
            if (radio.Playing)
            {
                radio.PauseAudio();
            }
        }
    }
    public void ResumeAudio()
    {
        foreach (RadioDialogo radio in radios)
        {
            if (radio.Playing)
            {
                radio.ResumeAudio();
            }
        }
    }
    
}
