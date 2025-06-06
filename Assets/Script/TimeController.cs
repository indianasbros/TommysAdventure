using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
public static TimeController Instance { get; private set; }

    [Header("Configuración")]
    [SerializeField] private float totalTimeSeconds = 3600f; // 1 hora
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining;
    private bool isRunning = true;

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
        timeRemaining = totalTimeSeconds;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isRunning) return;

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

    private void UpdateTimerDisplay()
    {
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", 
            time.Hours, time.Minutes, time.Seconds);
    }

    private void OnTimerEnd()
    {
        SceneManager.LoadScene("Defeat");
    }


    public void StopTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;
    public void ResetTimer()
    {
        timeRemaining = totalTimeSeconds;
        isRunning = true;
    }
}
