using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitKey : MonoBehaviour
{
    public bool canTake;
    private KeyCode takeKey = KeyCode.R;

    void Start()
    {
        //Control Setting for Take
        if (PlayerPrefs.HasKey("Key_2"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_2"), true, out var parsedKey))
            {
                takeKey = parsedKey;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(takeKey) && canTake)
        {
            //se añade al inventario
            Destroy(gameObject, 0.1f);
        }
    }
    
}
