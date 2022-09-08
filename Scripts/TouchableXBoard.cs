using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchableXBoard : MonoBehaviour
{
    [SerializeField] private float coefficient;

    [SerializeField] private Text coefficientText;

    private void Start()
    {
        SetCoefficientText();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IStackable>()?.TouchXBoard(ChangeCameraTargetAndDisappear);
    }

    private void ChangeCameraTargetAndDisappear()
    {
        GetComponent<BoxCollider>().enabled = false;
        GameManager.instance.SetState(GameStates.InBonusArea);
        GameManager.instance.SetEfficient(coefficient);
        Actions.act_brickTouchedXBoard?.Invoke(transform.position);
        gameObject.SetActive(false);
    }

    private void SetCoefficientText()
    {
        coefficientText.text = "x"+coefficient.ToString();
    }

}
