using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitShop : MonoBehaviour
{
    public void ExitShopp()
    {
        SceneManager.LoadScene("Kiara"); // Cambia "Kiara" por el nombre de la escena principal
        Debug.Log("Saliendo de la tienda y volviendo a la escena principal.");
        TimeController.Instance.isRunning = true;
    }
    


}
