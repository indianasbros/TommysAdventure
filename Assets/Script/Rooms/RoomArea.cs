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
    bool playerInRoom;
    public bool PlayerInRoom { get { return playerInRoom; } private set { playerInRoom = value; } }
    private void Reset()
    {
        roomCollider = GetComponent<Collider>();
        roomCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RoomManager.Instance != null && other.transform == RoomManager.Instance.Tommy)
        {
            PlayerInRoom = true;
            RoomManager.Instance.EnterRoom(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (RoomManager.Instance != null && other.transform == RoomManager.Instance.Tommy)
        {
            PlayerInRoom = false;
            RoomManager.Instance.ExitRoom(this);
        }
    }
}
