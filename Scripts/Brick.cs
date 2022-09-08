using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Brick : MonoBehaviour, IStackable, IChangableMaterialColor, IChangableColorCategory
{
    [SerializeField] private ColorCategory colorCategory;

    private ColorCategory firstColorCategory;

    private Stacker stacker;

    private MaterialManager materialManager;

    private MeshRenderer meshRenderer;

    private bool isCollect = false;

    private bool isFall = false;

    public Transform lastBrick;

    private Transform stackerTransform;


    private void Start()
    {

        firstColorCategory = colorCategory;

        stacker = Stacker.instance;

        materialManager = MaterialManager.instance;

        meshRenderer = GetComponent<MeshRenderer>();


        stackerTransform = stacker.transform;

        Actions.act_comboBarIsFull += ReturnColorToPlayerColor;

        Actions.act_comboBarIsEmpty += ReturnFirstColor;
        Actions.act_colorChanged += ChangeColorCategory;
    }




    public void Stack(Action<ColorCategory, Transform> action)
    {


        if (!isCollect)
        {
            action.Invoke(colorCategory, transform);
            Actions.act_trueBrickCollected += IncreaseHeight;
            Actions.act_falseBrickCollected += DecreaseHeight;
            Actions.act_playerTouchedToObstackle += StopFollowing;

            Actions.act_enteredBonusArea += StopFollowInBonusArea;

            isCollect = true;
        }
    }

    private void OnDestroy()
    {
        Actions.act_trueBrickCollected -= IncreaseHeight;
        Actions.act_falseBrickCollected -= DecreaseHeight;
        Actions.act_colorChanged -= ChangeColorCategory;
        Actions.act_playerTouchedToObstackle -= StopFollowing;

        Actions.act_comboBarIsFull -= ReturnColorToPlayerColor;
        Actions.act_comboBarIsEmpty -= ReturnFirstColor;

        Actions.act_enteredBonusArea -= StopFollowInBonusArea;


    }

    private void ReturnFirstColor()
    {
        if (!isCollect)
        {
            ChangeColorCategory(firstColorCategory);
        }
    }



    private void ReturnColorToPlayerColor()
    {
        if (!isCollect)
        {
            ChangeColorCategory(stacker.GetCategory());
        }

    }


    private void IncreaseHeight(int heightOfCollectedBrick)
    {
        transform.position += Vector3.up * 0.1f * heightOfCollectedBrick;
    }

    private void DecreaseHeight(int heightOfDestroyedBrick)
    {
        transform.position -= Vector3.up * 0.1f * heightOfDestroyedBrick;
    }



    private void StopFollowInBonusArea(float force)
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        float height = transform.position.y;

        rb.AddForce((transform.forward * height * force / 12 * 50f + 150 * transform.forward) * 2 );
    }

    private void StopFollowing()
    {
        if (isCollect && !isFall)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce((transform.forward * transform.position.y * 50f + 1500 * transform.forward));
            isFall = true;
        }

        Destroy(gameObject, 3f);
    }



    private void OnTriggerEnter(Collider other)         // obstacklea çarparsak
    {
        other.GetComponent<IObstackle>()?.Hit(TouchObstackle);
        other.GetComponent<ICollectable>()?.Collect();
    }

    private void TouchObstackle()
    {
        Actions.act_brickTouchedToObstackle?.Invoke(transform.localPosition);
    }


    public void ChangeMaterialColor(Color color)
    {
        if (isCollect || (!isCollect && ComboModeController.instance.GetIsComboMode()))
        {
            foreach (Transform item in transform)
            {
                item.GetComponent<MeshRenderer>().material.color = color;
            }
        }

    }


    public void TouchXBoard(Action action)
    {
        action.Invoke();
    }

    public void ChangeColorCategory(ColorCategory category)
    {
        this.colorCategory = category;

        Color newMaterialColor;

        switch (this.colorCategory)
        {
            case ColorCategory.Green:
                newMaterialColor = materialManager.Green.color;
                break;
            case ColorCategory.Red:
                newMaterialColor = materialManager.Red.color;
                break;
            case ColorCategory.Blue:
                newMaterialColor = materialManager.Blue.color;
                break;
            case ColorCategory.Pink:
                newMaterialColor = materialManager.Pink.color;
                break;
            default:
                newMaterialColor = materialManager.Green.color;
                break;
        }

        ChangeMaterialColor(newMaterialColor);
    }
}

