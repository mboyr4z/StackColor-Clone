using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions
{
    public static void RemoveFunctinFromAction(Action action, Action func)
    {
        action -= func;
    }

    public static Action<int> act_trueBrickCollected;

    public static Action<int> act_falseBrickCollected;

    public static Action<Vector3> act_brickTouchedToObstackle;

    public static Action act_playerTouchedToObstackle;

    public static Action<ColorCategory> act_colorChanged;

    public static Action act_goldCollected;

    public static Action act_comboBarIsFull;

    public static Action act_comboBarIsEmpty;

    public static Action act_runStarted;

    public static Action act_enteredFinisArea;

    public static Action<float> act_enteredBonusArea;

    public static Action<Vector3> act_brickTouchedXBoard;

    public static Action<float> act_finishGame;

    public static Action act_noBrickLeft;

    
}
