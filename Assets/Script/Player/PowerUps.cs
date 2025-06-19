using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps Instancia { get; private set; }

    public bool PowerUpSpeed;
    public bool PowerUpTime;
    public bool SpeedUI; //Usen esto cuando tengan el inventario, asi muestran los power upps :D
    public bool TimeUI;
    void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Singleton 'SingletonSpeed' inicializado y persistente.");
        }
        else
        {
            Debug.LogWarning("Ya existe una instancia de 'SingletonSpeed'. Destruyendo duplicado.");
            Destroy(gameObject);
        }
    }

}
    

