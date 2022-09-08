using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoSingleton<FinishArea>, IArea
{
    public void EnterArea(Action<AreaType> action)
    {
        action.Invoke(AreaType.Finish);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
