using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinGamePanel : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [SerializeField] private Text totalCoinText;

    [SerializeField] private Text levelText;

    [SerializeField] private Text coinText;

    

    private void OnEnable()
    {
        SetScoreText();
        SetTotalCoinText();
        SetLevelText();
        SetCoinText();
    }

    private void SetLevelText()
    {
        levelText.text = "Level " + (GameManager.instance.GetLevel() + 1) + " Completed..";
    }

    private void SetCoinText()
    {
        coinText.text =  (GameManager.instance.GetScore() * GameManager.instance.GetEfficient() ).ToString();
    }

    private void SetTotalCoinText()
    {
        totalCoinText.text = GameManager.instance.GetTotalCoin().ToString();
    }

    private void SetScoreText() {
        scoreText.text = GameManager.instance.GetScore().ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.IncreaseLevel();
            SceneManager.LoadScene(0);
        }
    }
}
