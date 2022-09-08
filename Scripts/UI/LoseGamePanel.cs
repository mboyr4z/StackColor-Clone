using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseGamePanel : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [SerializeField] private Text goldText;

    [SerializeField] private Text totalCoinText;

    private void SetScoreText()
    {
        scoreText.text = "148";
    }

    private void SetGoldText()
    {
        goldText.text = "+148";
    }

    private void SetTotalCoinText()
    {
        totalCoinText.text = "15000";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }


}
