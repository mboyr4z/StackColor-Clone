using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerBox : MonoBehaviour
{
    public void IncreaseScale(float increaseValue)
    {
        transform.localScale += Vector3.up * increaseValue * 0.1f;
    }

    public void DecreaseScale(float deincreaseValue)
    {
        transform.localScale += Vector3.down * deincreaseValue *0.1f;
    }
}
