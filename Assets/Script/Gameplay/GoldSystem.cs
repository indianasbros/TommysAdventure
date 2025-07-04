using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    private static GoldSystem instance;
    public static GoldSystem Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("GoldSystem");
                instance = obj.AddComponent<GoldSystem>();
            }
            return instance;
        }
    }
    public int Gold = 1000;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
