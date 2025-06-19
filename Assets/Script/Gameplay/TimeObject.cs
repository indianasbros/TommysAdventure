using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public static TimeObject Instancia;
    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

}
