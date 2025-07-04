using UnityEngine;


public class BotonManager : MonoBehaviour
{
    public void ComprarVelocidad(ItemData powerUpSpeed)
    {
        int oro = GoldSystem.Instance.Gold;
        if (!PowerUps.Instancia.HasPowerUp(powerUpSpeed) && oro >= 500)
        {
            GoldSystem.Instance.Gold = oro - 500;
            PowerUps.Instancia.AddPowerUp(powerUpSpeed);
            PowerUps.Instancia.SpeedUI = true;
        }
    }

    public void ComprarTiempoExtra(ItemData powerUpTime)
    {
        int oro = GoldSystem.Instance.Gold;
        
        if (!PowerUps.Instancia.HasPowerUp(powerUpTime) && oro >= 1000)
        {
            GoldSystem.Instance.Gold -= 1000;
            PowerUps.Instancia.AddPowerUp(powerUpTime);
            PowerUps.Instancia.TimeUI = true;
        }
    }
}
