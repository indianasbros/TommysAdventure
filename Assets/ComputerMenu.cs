using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerMenu : MonoBehaviour
{
    private KeyCode interactKey = KeyCode.E;
    bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        canInteract = false;
        if (PlayerPrefs.HasKey("Key_0"))
        {
            if (Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
            {
                interactKey = parsedKey;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            SceneManager.LoadScene("UITienda");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
            
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
