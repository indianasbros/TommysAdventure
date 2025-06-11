using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitKey : MonoBehaviour
{
    public bool canTake;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canTake)
        {
            //se añade al inventario
            Destroy(gameObject, 0.1f);
        }
    }
}
