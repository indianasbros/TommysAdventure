using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private string roomName;
    [SerializeField] private AudioClip roomMusic;
    [SerializeField] private AudioMixerGroup mixerGroup;
    public AudioMixerGroup MixerGroup => mixerGroup;

    private static AudioSource globalRoomAudio;
    private static string currentRoom = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Tommy")
            return;

        if (currentRoom == roomName)
            return;

        if (globalRoomAudio == null)
        {
            globalRoomAudio = new GameObject("RoomAudio").AddComponent<AudioSource>();
            DontDestroyOnLoad(globalRoomAudio.gameObject);
            globalRoomAudio.loop = true;
            globalRoomAudio.volume = 1f;
            globalRoomAudio.spatialBlend = 0f;
        }

        globalRoomAudio.Stop();
        globalRoomAudio.clip = roomMusic;

        if (mixerGroup != null)
            globalRoomAudio.outputAudioMixerGroup = mixerGroup;

        globalRoomAudio.Play();
        currentRoom = roomName;

    }
}