using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RingCode : MonoBehaviour
{
    public bool canTake;

    private KeyCode interactKey = KeyCode.E;


    void Start()
    {
        canTake = false;

        //Control Setting for Interact
        if (PlayerPrefs.HasKey("Key_0"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
            {
                interactKey = parsedKey;
            }
        }
    }  


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && canTake)
        {
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
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTake = false;
        }
    }
}
