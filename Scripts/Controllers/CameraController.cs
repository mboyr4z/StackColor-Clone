using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] private Transform player;

    [SerializeField] private float katsayi;

    [SerializeField] private float finishAreaFollowTime;

    private Vector3 lastTouchedBoard;

    private GameManager gameManager;

    private float height;

    private Vector3 stackerOffset;

    private Vector3 stackerOffsetInFinishArea;

    private Vector3 stackerOffsetInBonusArea = new Vector3(0.73f, -7.13f, 11.38f);

    private float efficient = 0;


    private void Start()
    {
        gameManager = GameManager.instance;

        stackerOffset = player.position - transform.position;

        stackerOffsetInFinishArea = player.position - new Vector3(2.76f, 3.61f, -3.51f);


        Actions.act_trueBrickCollected += SetCameraHeightByCollectedBricks;
        Actions.act_brickTouchedXBoard += SetLastTouchedBoard;
        Actions.act_enteredBonusArea += RotateCamInBonusArea;
    }

    private void RotateCamInBonusArea(float force) {
        transform.DORotateQuaternion(Quaternion.Euler(22.037f, 2.721f, 0.134f), 0.1f);
    }

    private void SetLastTouchedBoard(Vector3 pos)
    {
        this.lastTouchedBoard = pos;
    }
    private void OnDestroy()
    {
        Actions.act_trueBrickCollected -= SetCameraHeightByCollectedBricks;
        Actions.act_brickTouchedXBoard -= SetLastTouchedBoard;
        Actions.act_enteredBonusArea -= RotateCamInBonusArea;
    }



    public void SetCameraHeightByCollectedBricks(int heightOfCollectedBrick)
    {
        stackerOffset -= new Vector3(0,0.4f,-0.1f) * heightOfCollectedBrick * katsayi;
    }

    private void LateUpdate()
    {
        if(gameManager.State == GameStates.InBonusArea)
        {
            FollowXBoard();
        }
        
        if(gameManager.State == GameStates.InFinishArea)
        {
             FollowPickerInFinishArea();
        }

        if(gameManager.State == GameStates.InRun)
        {
            FollowPicker();
        }
    }

    private void FollowPickerInFinishArea()
    {
        transform.position = Vector3.Slerp(transform.position, player.position - stackerOffsetInFinishArea,  finishAreaFollowTime);
    }

    private void FollowPicker()
    {
        transform.position = Vector3.Slerp(transform.position, player.position - stackerOffset, 0.1f);
    }


    private void FollowXBoard()
    {
        float dist = Vector3.Distance(lastTouchedBoard, transform.position);
        if (dist < 13.5f)
        {
            gameManager.SetState(GameStates.InWinPanel);

            CanvasManager.instance.OpenMenu("winGamePanel");
            Actions.act_finishGame?.Invoke(GameManager.instance.GetEfficient());
            return;
        }

        if(lastTouchedBoard != Vector3.zero)
        {
            transform.position = Vector3.Slerp(transform.position, lastTouchedBoard - stackerOffsetInBonusArea, 0.01f);
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, player.position + new Vector3(0,10f,1f), Time.deltaTime);
        }
        
    }
  
}
