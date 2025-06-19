using UnityEngine;


public class BotonManager : MonoBehaviour
{
    
    public PowerUps PowerUps;
    public GoldSystem GoldSystem;
    public void ComprarVelocidad()
    {
        int oro = GameObject.Find("GoldSystem").GetComponent<GoldSystem>().Gold;
        Debug.Log("Oro disponible: " + oro);
        if (!PowerUps.Instancia.SpeedUI && oro >= 500)
        {
            GameObject.Find("GoldSystem").GetComponent<GoldSystem>().Gold = oro - 500;
            PowerUps.Instancia.PowerUpSpeed = true;
            PowerUps.Instancia.SpeedUI = true;
        }
    }

    public void ComprarTiempoExtra()
    {
        Debug.Log("Intentando comprar tiempo extra...");
        int oro = GameObject.Find("GoldSystem").GetComponent<GoldSystem>().Gold;
        Debug.Log("Oro disponible: " + oro);
        if (!PowerUps.Instancia.TimeUI && oro >= 1000)
        {
            GameObject.Find("GoldSystem").GetComponent<GoldSystem>().Gold -= 1000;
            PowerUps.Instancia.PowerUpTime = true;
            PowerUps.Instancia.TimeUI = true;
        }
    }
}
