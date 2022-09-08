using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable {
    public void Stack(Action<ColorCategory, Transform> action);

    public void TouchXBoard(Action action);
}
