using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldSys : MonoBehaviour
{
    public TextMeshProUGUI texto;
    
    private void Update()
    {
        if (GoldSystem.Instance == null)
        {
            return; // Asegurarse de que GoldSystem esté inicializado
        }

        texto.text = GoldSystem.Instance.Gold.ToString();
    }
}
