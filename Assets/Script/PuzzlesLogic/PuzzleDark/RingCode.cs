using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RingCode : MonoBehaviour
{
    public bool canTake;
    void Start()
    {
        canTake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canTake)
        {
            //se añade al inventario
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
