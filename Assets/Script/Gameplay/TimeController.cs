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
    [Header("UI")]
    [SerializeField] private GameObject timerPanel;
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
            Debug.Log("activo el timer");
            timerPanel.SetActive(true);
            SetDifficulty(gameDifficulty);
            UpdateTimerDisplay();
        }
        if (PowerUps.Instancia.PowerUpTime)
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
        //PowerUps.Instancia.PowerUpTime = false;
    }
    public void DiscartPowerUp()
    {
        //PowerUps.Instancia.PowerUpTime = false;
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
    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                timeRemaining = Mathf.Infinity;
                isRunning = false;
                timerPanel.SetActive(true);
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