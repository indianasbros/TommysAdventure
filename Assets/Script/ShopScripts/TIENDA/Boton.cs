using UnityEngine;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        Debug.Log("¡Botón presionado!");
    }
}