using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps Instancia { get; private set; }
    public Dictionary<ItemData,bool> PowerUpsList = new ();
    void Start()
    {
        SpeedUI = false;
        TimeUI = false;
    }
    public bool HasPowerUp(ItemData powerUp)
    {
        if (PowerUpsList.ContainsKey(powerUp))
        {
            return PowerUpsList[powerUp];
        }
        return false;
    }
    public bool HasPowerUp(string powerUp)
    {
        ItemData itemData = Resources.Load<ItemData>("Items/" + powerUp);
        if (PowerUpsList.ContainsKey(itemData))
        {
            return PowerUpsList[itemData];
        }
        return false;
    }
    public void ResetPowerUps()
    {
        PowerUpsList.Clear();
        SpeedUI = false;
        TimeUI = false;
    }
    public void SetPowerUpActive(ItemData powerUp, bool active = true)
    {
        if (PowerUpsList.ContainsKey(powerUp))
        {
            PowerUpsList[powerUp] = active;
        }
    }
    public void RemovePowerUp(ItemData powerUp)
    {

        if (PowerUpsList.ContainsKey(powerUp))
        {
            PowerUpsList.Remove(powerUp);
        }
    }
    public void AddPowerUp(ItemData powerUp)
    {
        
        if (!PowerUpsList.ContainsKey(powerUp))
        {
            PowerUpsList.Add(powerUp,true);
        }
    }
    public bool SpeedUI;
    public bool TimeUI;
    void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
}
    

