using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }
    public enum Difficulty { Easy, Normal, Hard }

    [Header("Configuración")]
    [SerializeField] private Difficulty gameDifficulty = Difficulty.Normal;
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining;
    public bool isRunning;

    public float TimeRemaining => timeRemaining;
    public bool IsRunning => isRunning;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetDifficulty(gameDifficulty);
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isRunning) return;
        
        if (PowerUps.Instancia.PowerUpTime)
        {
            applyPowerUp();
        }
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timeRemaining = 0;
            isRunning = false;
            UpdateTimerDisplay();
            OnTimerEnd();
        }
    }
    private void applyPowerUp()
    {
        timeRemaining += 900f; //15 minutos por power up XD
        PowerUps.Instancia.PowerUpTime = false; //aca hagan cambios de ui si quieren pero no creo que sea necesario, en caso de que no lo hagan borren el TimeGUI
    }
    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                timeRemaining = Mathf.Infinity;
                isRunning = false;
                timerText.text = "∞";
                break;

            case Difficulty.Normal:
                timeRemaining = 3600f; // 1 hora
                isRunning = true;
                break;

            case Difficulty.Hard:
                timeRemaining = 1800f; // 30 minutos
                isRunning = true;
                break;
        }
    }

    private void UpdateTimerDisplay()
    {
        if (float.IsInfinity(timeRemaining))
        {
            timerText.text = "∞";
            return;
        }

        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
            time.Hours, time.Minutes, time.Seconds);
    }

    private void OnTimerEnd()
    {
        Debug.Log("¡Se acabó el tiempo!");

    }

    public void StopTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;

    public void ResetTimer()
    {
        SetDifficulty(gameDifficulty);
    }

    public void SetGameDifficulty(Difficulty newDifficulty)
    {
        gameDifficulty = newDifficulty;
        ResetTimer();
    }
}