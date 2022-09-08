using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : MonoSingleton<FinishPanel>
{

    [SerializeField] private Slider slider;

    [SerializeField] private float decreaseValue;

    [SerializeField] private float decreaseTime;

    [SerializeField] private float increaseValue;

    private float counter = 0;

    public float Counter { get => counter; set => counter = value; }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IncreaseCounter();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DecreaseCounter(decreaseTime));
    }

    IEnumerator DecreaseCounter(float time)
    {
        yield return new WaitForSeconds(time);
        MovementController.instance.IncreaseSpeed(Counter * 2);
        Counter -=decreaseValue;
        Counter = Mathf.Clamp(Counter, 0, 12);
        SetSlider();
        StartCoroutine(DecreaseCounter(time));
    }




    private void IncreaseCounter()
    {
        Counter+= increaseValue;
        Counter = Mathf.Clamp(Counter, 0, 12);

        MovementController.instance.IncreaseSpeed(Counter * 2);

        SetSlider();
    }

    private void SetSlider()
    {
        slider.value = Counter / (float)12;
    }


    
}
