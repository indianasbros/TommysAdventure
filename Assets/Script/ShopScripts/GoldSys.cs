using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldSys : MonoBehaviour
{
    private TextMeshProUGUI texto;
    GoldSystem GoldSystem;
    private void Update()
    {
        GoldSystem = GameObject.Find("GoldSystem").GetComponent<GoldSystem>();
        texto = GetComponent<TextMeshProUGUI>();
        texto.text = GoldSystem.Gold.ToString();
    }
}
