using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementController : MonoSingleton<MovementController>
{

    [SerializeField] private float horizontalMovementSensitivity;

    [SerializeField] private float movementSpeed;

    private GameManager gameManager;

    private Vector2 firstMousePos;

    private Vector2 lastMousePos;





    private float firstSpeed;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    private void Start()
    {

        gameManager = GameManager.instance;
        firstSpeed = MovementSpeed;


        Actions.act_brickTouchedToObstackle += ResetSpeed;
        Actions.act_playerTouchedToObstackle += StopMovementThanTime;
        Actions.act_enteredFinisArea += ResetSpeedAndGoMidLine;
    }


    private void OnDestroy()
    {
        Actions.act_brickTouchedToObstackle -= ResetSpeed;
        Actions.act_playerTouchedToObstackle -= StopMovementThanTime;
        Actions.act_enteredFinisArea -= ResetSpeedAndGoMidLine;
    }

    public void IncreaseSpeed(float IncreaseValue)
    {
        movementSpeed = firstSpeed + IncreaseValue;
    }

    private void ResetSpeedAndGoMidLine()
    {
        ResetSpeed();
        transform.DOLocalMoveX(0, 0.1f);
    }
  

    
    private void StopMovementThanTime()
    {
        Invoke(nameof(StopMovement), 0.05f);
    }

    private void StopMovement()
    {
        movementSpeed = 0;
    }

    private void ResetSpeed(Vector3 obj)
    {
        Invoke(nameof(ResetSpeed),0.05f);
    }

    private void ResetSpeed()
    {
        MovementSpeed = firstSpeed / 3;
    }
    

    void Update()
    {
      
        if(gameManager.State == GameStates.InFinishArea)
        {
            MoveForward();
        }

        if (gameManager.State == GameStates.InRun)
        {
            MovementSpeed += Time.deltaTime * 2;
            MovementSpeed = Mathf.Clamp(MovementSpeed, 0, firstSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                firstMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                lastMousePos = Input.mousePosition;
                MoveHorizontal((lastMousePos - firstMousePos).x);
            }
            MoveForward();
        }

    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * MovementSpeed;
    }

    private void MoveHorizontal(float distanceMousePoses)
    {
        transform.position += Vector3.right * distanceMousePoses * Time.deltaTime * horizontalMovementSensitivity;
        Vector3 tp = transform.position;
        transform.position = new Vector3(Mathf.Clamp(tp.x,-3,3), tp.y, tp.z);
    }
}
