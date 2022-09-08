using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusArea : MonoBehaviour, IArea
{
    public void EnterArea(Action<AreaType> action)
    {
        action.Invoke(AreaType.Bonus);
    }
}
