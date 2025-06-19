using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheets : MonoBehaviour
{
    [SerializeField] public GameObject sheetSlice;
    private bool hasItem;
    public bool HasItem { get { return hasItem; } set {hasItem = value; } }
 
    void Start()
    {
        sheetSlice.SetActive(false);
    }

    void Update()
    {
        if (hasItem)
        {
            sheetSlice.SetActive(true);
        }
    }
}
