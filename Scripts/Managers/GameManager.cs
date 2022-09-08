using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private int totalCoin;

    private int collectedBrickCount;

    private int level;

    private int score = 0;

    private float efficient = 0;

    private GameStates state;

    

    private void Start()
    {
        collectedBrickCount = 0;
        SetState(GameStates.InWaitPanel);
    }

    public GameStates State { get => state;  }

    public float GetEfficient()
    {
        return efficient;
    }

    public void SetEfficient(float value)
    {
        efficient = value;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetState(GameStates state)
    {
        this.state = state;
    }



    public void IncreaseScore(int increaseValue)
    {
        score += increaseValue;
    }

 

    public int GetLevel()
    {
          return PlayerPrefs.GetInt(GameVariables.Level.ToString());
    }

    public void SetLevel(int value)
    {
        PlayerPrefs.SetInt(GameVariables.Level.ToString(), value);
    }

    public void SetTotalCoin(int value)
    {
        PlayerPrefs.SetInt(GameVariables.TotalCoin.ToString(), value);
    }

    public int GetTotalCoin()
    {
        return PlayerPrefs.GetInt(GameVariables.TotalCoin.ToString());
    }

    public void IncreaseCoin(int value)
    {
        SetTotalCoin(GetTotalCoin() + value);
    }

    public void IncreaseLevel()
    {
        SetLevel(GetLevel() + 1);
    }

}
