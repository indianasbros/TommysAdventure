using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class ExitKey : MonoBehaviour
{
    public bool canTake;
    private AudioSource audioSource;
    [SerializeField] private KeyCheck check;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canTake)
        {
            //se añade al inventario
            check.AddInput(canTake, null); // Aquí puedes pasar el AudioClip correspondiente si es necesario
            Destroy(gameObject, 0.1f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform holdPoint = GameObjectExtensions.FindComponentInChildWithTag<Transform>(other.gameObject,"HoldPoint");
            
            ScissorsController scissorsController = GetComponentInChildren<ScissorsController>(holdPoint.gameObject);
            if (scissorsController)
            {
                canTake = true;
            }
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
