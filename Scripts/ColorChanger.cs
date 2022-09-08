using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour, IColorChangable
{
    [SerializeField] private ColorCategory colorCategory;

    public void ChangeColor(Action<ColorCategory> action)
    {
        action.Invoke(colorCategory);
    }
}
