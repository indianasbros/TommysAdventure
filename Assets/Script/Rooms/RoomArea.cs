using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomArea : MonoBehaviour
{
    [SerializeField] private AudioClip roomMusic;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private Collider roomCollider;

    public AudioClip RoomMusic => roomMusic;
    public AudioMixerGroup MixerGroup => mixerGroup;
    public Collider RoomCollider => roomCollider;

    private void Reset()
    {
        roomCollider = GetComponent<Collider>();
        roomCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RoomManager.Instance != null && other.transform == RoomManager.Instance.Tommy)
        {
            RoomManager.Instance.EnterRoom(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (RoomManager.Instance != null && other.transform == RoomManager.Instance.Tommy)
        {
            RoomManager.Instance.ExitRoom(this);
        }
    }
}
