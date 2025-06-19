using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObjects : MonoBehaviour
{
    WaterLevel waterPuzzle;
    public bool canTake;

    private KeyCode takeKey = KeyCode.R;

    void Start()
    {
        canTake = false;
        if (waterPuzzle == null)
        {
            waterPuzzle = FindObjectOfType<WaterLevel>();
        }
        if (waterPuzzle != null)
        {
            Debug.Log("se encontro");
        }
        if (waterPuzzle == null)
        {
            Debug.LogError("No se encontró WaterLevel en la escena.");
        }

        //Control Setting for Take
        if (PlayerPrefs.HasKey("Key_2"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_2"), true, out var parsedKey))
            {
                takeKey = parsedKey;
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(takeKey) && canTake)
        {
            Debug.Log("agarre");

            waterPuzzle.objectTaked = true;
            Destroy(gameObject, 0.1f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
