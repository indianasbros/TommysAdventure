using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Assets.Script.Difficulty;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }
    [Header("UI")]
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining;
    public bool isRunning;

    public float TimeRemaining => timeRemaining;
    public bool IsRunning => isRunning;
    [SerializeField] ItemData powerUpTime;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
    }
    void OnEnable()
    {
        RoomManager.Instance.OnEnteredFirstPuzzle += OnEnteredFirstPuzzle;
    }
    void OnDisable()
    {
        RoomManager.Instance.OnEnteredFirstPuzzle -= OnEnteredFirstPuzzle;
    }
    void OnEnteredFirstPuzzle()
    {
        isRunning = true;
    }
    void Start()
    {
        isRunning = false;
        timerPanel.SetActive(false);
        
    }

    void Update()
    {
        if (!IsRunning) return;
        if (IsRunning && !timerPanel.activeSelf)
        {
            ApplyDifficulty();
            UpdateTimerDisplay();
        }
        if (PowerUps.Instancia.HasPowerUp(powerUpTime))
        {
            ApplyPowerUp();
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
    private void ApplyPowerUp()
    {
        timeRemaining += 900f; //15 minutos
        //PowerUps.Instancia.SetPowerUpActive(powerUpTime, false);
    }
    public void DiscartPowerUp()
    {
        PowerUps.Instancia.SetPowerUpActive(powerUpTime, false);
        timeRemaining -= 900f; //15 minutos
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
            isRunning = false;
            UpdateTimerDisplay();
            OnTimerEnd();
        }
        UpdateTimerDisplay();
    }
    

    private void UpdateTimerDisplay()
    {
        if (!timerPanel.activeSelf) return;
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
        GameplayManager.Instance.GameOver();
    }

    public void StopTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;
    private void ApplyDifficulty()
    {
        Difficulty dif = DifficultyController.Instance.GameDifficulty;
        switch (dif)
        {
            case Difficulty.Easy:
                timeRemaining = Mathf.Infinity;
                isRunning = false;
                timerPanel.SetActive(true);
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
        timerPanel.SetActive(true);
    }
    public void ResetTimer()
    {
        ApplyDifficulty();
    }

    
}