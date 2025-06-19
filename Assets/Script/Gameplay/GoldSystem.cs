using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    public GoldSystem Instancia;
    public int Gold = 1000;
    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}
