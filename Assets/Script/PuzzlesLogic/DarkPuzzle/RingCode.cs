using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RingCode : MonoBehaviour
{

    public bool canTake;
    [SerializeField] private AudioSource ringSound;

    void Start()
    {
        canTake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTake)
        {

            //Sound Heart Stop
            if (ringSound.isPlaying)
            {
                ringSound.Stop();
            }

            //se añade al inventario
            Destroy(gameObject, 0.1f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("estoy colisionando con " + other.name);
            canTake = true;

            //Sound Heart Start
            if (!ringSound.isPlaying)
            {
                ringSound.Play();
            }                
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTake = false;
        }

        //Stop heart music
        if (ringSound.isPlaying)
        {
            ringSound.Stop();
        }            
    }
}
