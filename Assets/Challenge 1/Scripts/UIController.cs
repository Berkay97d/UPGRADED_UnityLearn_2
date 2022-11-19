using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text cointCounterText;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private TMP_Text endGameCoinCountText;
    
    
    [SerializeField] private Canvas endGame;
    private static int coinCount;
    private int oldBestScore;
    
    [SerializeField] private float TimeLeft;
    [SerializeField] private bool TimerOn = false;
    [SerializeField] private TMP_Text TimerTxt;

    public bool isTimeUp()
    {
        if (TimeLeft <= 0)
        {
            return true;
        }

        return false;
    }
    
    private void GetHighScore()
    {
        oldBestScore = PlayerPrefs.GetInt("Best_Score", 0);
        if (coinCount > oldBestScore)
        {
            PlayerPrefs.SetInt("Best_Score", coinCount);
        }
    }
    private void Update()
    {
        UpdateCointCount();
        if(TimerOn)
        {
            
            if(TimeLeft > 0 && !endGame.gameObject.activeSelf)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    private void UpdateCointCount()
    {
        GetHighScore();
        cointCounterText.text = "Coin: " + coinCount;
        bestScoreText.text = "Best Score: " + oldBestScore;
        endGameCoinCountText.text = "Score: " + coinCount;
    }
    
    public static void WinCoin()
    {
        coinCount++;
    }

    public void Restart()
    {
        endGame.gameObject.SetActive(false);
        TimeLeft = 60;
        TimerOn = true;
        coinCount = 0;
    }
    
    void Start()
    {
        TimerOn = true;
    }

    

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
}
