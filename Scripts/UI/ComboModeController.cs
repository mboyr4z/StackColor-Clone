using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboModeController : MonoSingleton<ComboModeController>
{
    [SerializeField] private Slider comboSlider;

    [SerializeField] private Image fillAreaOfSlider;

    [SerializeField] private Color comboColorOfFillArea;

    [SerializeField] private Color startFillAreaColor;

    [SerializeField] private int comboConstantValue;

    private int counter = 0;

    private bool comboMode = false;

    void Start()
    {
        Actions.act_trueBrickCollected += IncreaseComboCounter;
        Actions.act_falseBrickCollected += ResetComboCounter;
        Actions.act_brickTouchedToObstackle += ResetComboCounter;
    }

    private void OnDestroy()
    {
        Actions.act_trueBrickCollected -= IncreaseComboCounter;
        Actions.act_falseBrickCollected -= ResetComboCounter;
        Actions.act_brickTouchedToObstackle -= ResetComboCounter;
    }

    public bool GetIsComboMode()
    {
        return comboMode;
    }

    private void OnEnable()
    {
        SetColorOfFillArea(startFillAreaColor);
    }


    private void ResetComboCounter<T>(T unknownType)
    {
        counter = 0;
        CheckComboMode();
    }

    private void IncreaseComboCounter(int increaseValue)
    {
        if (!comboMode)
        {
            counter += increaseValue;
            CheckComboMode();
        }
    }

    private void CheckComboMode()
    {

        if (counter >= comboConstantValue)
        {
            comboMode = true;
            ComboMode(5f);

 
        }
        else if (counter > 5)
        {
            comboSlider.gameObject.SetActive(true);
            comboSlider.value = ((float)counter / (float)comboConstantValue);
        }
        else
        {
            comboSlider.gameObject.SetActive(false);
        }
    }

    private void ComboMode(float totalDecreaseTime)
    {
        SetColorOfFillArea(comboColorOfFillArea);
        float replayRate = totalDecreaseTime / comboConstantValue;

        Actions.act_comboBarIsFull?.Invoke();
        for (int i = 0; i < comboConstantValue; i++)
        {
            StartCoroutine(DecreaseSlider(replayRate * i));
        }


    }

    private void SetColorOfFillArea(Color color)
    {
        fillAreaOfSlider.color = color;
    }

    IEnumerator DecreaseSlider(float time)
    {
        yield return new WaitForSeconds(time);
        counter--;
        comboSlider.value = comboSlider.value = ((float)counter / (float)comboConstantValue);

        if (counter == 0)
        {

            StopAllCoroutines();
            SetColorOfFillArea(startFillAreaColor);
            Actions.act_comboBarIsEmpty?.Invoke();
            comboMode = false;
            comboSlider.gameObject.SetActive(false);
        }

    }


}
