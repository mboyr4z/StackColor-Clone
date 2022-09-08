using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstackle : MonoBehaviour, IObstackle 
{
    [SerializeField] private Direction direction;

    [SerializeField] private float movementDistance;

    private void Start()
    {
        Actions.act_comboBarIsFull += DisableObstackle;
        Actions.act_comboBarIsEmpty += EnableObstackle;
    }

    private void OnDestroy()
    {
        Actions.act_comboBarIsFull -= DisableObstackle;
        Actions.act_comboBarIsEmpty -= EnableObstackle;
    }

    private void EnableObstackle()
    {
        GetComponent<BoxCollider>().enabled = true;
        if (direction == Direction.Vertical)
        {
            transform.DOLocalMoveY(transform.localPosition.y + movementDistance, 1f);
        }
        else
        {
            transform.DOLocalMoveX(transform.localPosition.x + movementDistance, 1f);
        }

        
    }

    private void DisableObstackle()
    {
        GetComponent<BoxCollider>().enabled = false;
        if (direction == Direction.Vertical)
        {
            transform.DOLocalMoveY(transform.localPosition.y - movementDistance, 1f);
        }
        else
        {
            transform.DOLocalMoveX(transform.localPosition.x - movementDistance, 1f);
        }
    }

    public void Hit(Action action)
    {
        GetComponent<BoxCollider>().enabled = false;
        action.Invoke();
    }
}

