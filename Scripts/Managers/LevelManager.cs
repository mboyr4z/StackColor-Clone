using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{

    private GameManager gameManager;

    private ColorCategory startingCategory;

    private Color roadColor;

    private Color cameraColor;

    private Vector3 startingPoint;



    private void Start()
    {
        gameManager = GameManager.instance;
        createLevel();
    }
    private void createLevel()
    {
        DestroyLevel();

        Level newLevel = (Level)Resources.Load("Level" + ((gameManager.GetLevel() % GetTotalLevelCount()) + 1).ToString());

        roadColor = newLevel.levelTheme.roadColor;
        cameraColor = newLevel.levelTheme.camColor;
        startingCategory = newLevel.startingCategory;
        startingPoint = newLevel.startingPoint;

        GameObject createdLevel = Instantiate(newLevel.prefab);

        ChangeRoadColor(createdLevel.transform);
        ChangeCameraBackgroundColor(cameraColor);
        ChangeStartPositionOfPlayer(startingPoint);
        ChangeColorCategoryOfPlayer(startingCategory);
    }

    private void ChangeColorCategoryOfPlayer(ColorCategory startingCategory)
    {
        Stacker.instance.ChangeColorCategory(startingCategory);
    }

    private void ChangeStartPositionOfPlayer(Vector3 startingPoint)
    {
        MovementController.instance.transform.position = startingPoint;
    }

    private void ChangeCameraBackgroundColor(Color color)
    {
        Camera.main.backgroundColor = color;
    }

    private void ChangeRoadColor(Transform levelParent)
    {
        Transform Road = levelParent.GetChild(0);

        foreach (Transform roadPiece in Road)
        {
            roadPiece.GetComponent<MeshRenderer>().material.color = roadColor;
        }
    }

    private void DestroyLevel()
    {
        if(transform.GetChildCount() > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        
    }

    private int GetTotalLevelCount()
    {
        int count;

        for (count = 1; ; count++)
        {
            if (Resources.Load("Level" + count))
            {
            }
            else
            {
                break;
            }

        }
        return count - 1;
    }
}
