using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheets : MonoBehaviour
{
    [SerializeField] public GameObject sheetSlice;
    bool hasItem;
    public bool HasItem { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        sheetSlice.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasItem)
        {
            sheetSlice.SetActive(true);
        }
    }
}
