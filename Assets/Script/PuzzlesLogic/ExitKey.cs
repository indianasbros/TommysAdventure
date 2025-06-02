using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitKey : MonoBehaviour
{
    GameObject gameObject;
    public bool canTake;
    void Start()
    {
        gameObject = GetComponent<GameObject>();
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
