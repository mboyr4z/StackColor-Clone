using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitPanel : MonoBehaviour
{
    [SerializeField] private Text levelText;

    private CanvasManager canvasManager;

    private void Start()
    {
        canvasManager = CanvasManager.instance;
    }

    private void SetLevelText()
    {
        levelText.text = "Level " + (GameManager.instance.GetLevel() + 1).ToString();
    }

    private void OnEnable()
    {
        SetLevelText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.SetState(GameStates.InRun);
            canvasManager.OpenMenu("inGamePanel");
            
            Actions.act_runStarted?.Invoke();
        }
    }
}
