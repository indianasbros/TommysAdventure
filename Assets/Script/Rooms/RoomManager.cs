using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    [SerializeField] private Transform tommy;
    public Transform Tommy => tommy;
    public event Action OnEnteredFirstPuzzle;
    private static AudioSource globalAudio;
    private RoomArea currentRoom;

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

    void Start()
    {
        if (globalAudio == null)
        {
            GameObject audioObj = new GameObject("RoomMusic");
            DontDestroyOnLoad(audioObj);
            globalAudio = audioObj.AddComponent<AudioSource>();
            globalAudio.loop = true;
            globalAudio.volume = 1f;
            globalAudio.spatialBlend = 0f;
        }
    }

    public void EnterRoom(RoomArea room)
    {
        if (currentRoom != room)
        {
            currentRoom = room;
            ChangeMusic(room);
            if(currentRoom.IsFirstPuzzle && OnEnteredFirstPuzzle != null)
            {
                Debug.Log("Entered first puzzle room");
                OnEnteredFirstPuzzle?.Invoke();
            }
        }
    }
    public void ExitRoom(RoomArea room)
    {
        if (currentRoom == room)
        {
            globalAudio.Stop();
            currentRoom = null;
        }
    }

    private void ChangeMusic(RoomArea room)
    {
        globalAudio.Stop();
        globalAudio.clip = room.RoomMusic;
        globalAudio.outputAudioMixerGroup = room.MixerGroup;
        globalAudio.Play();
    }
    public void PauseMusic()
    {
        globalAudio.Pause();
    }
    public void StopMusic()
    {
        globalAudio.Stop();
    }
    public void ResumeMusic()
    {
        globalAudio.Play();
    }
}
