using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stacker : MonoSingleton<Stacker>, IChangableMaterialColor, IChangableColorCategory
{
    [SerializeField] private ColorCategory category;

    [SerializeField] private MeshRenderer[] pickerParts;

    [SerializeField] private List<Transform> collectedBricks;

    private GameManager gameManager;

    private CanvasManager canvasManager;

    private MaterialManager materialManager;

    private CameraController cameraController;
 

    private MeshRenderer meshRenderer;

    private float lastChangedColorTime = -5f;

    public ColorCategory GetCategory()
    {
        return category;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        canvasManager = CanvasManager.instance;
        materialManager = MaterialManager.instance;
        cameraController = CameraController.instance;

        meshRenderer = GetComponent<MeshRenderer>();

        collectedBricks = new List<Transform>();

        Actions.act_comboBarIsFull += IncreaseScale;
        Actions.act_comboBarIsEmpty += SetFirstScale;
        Actions.act_brickTouchedToObstackle += FallBricks;
    }

    private void OnDestroy()
    {
        Actions.act_comboBarIsFull -= IncreaseScale;
        Actions.act_comboBarIsEmpty -= SetFirstScale;
        Actions.act_brickTouchedToObstackle -= FallBricks;

    }

    private void SetFirstScale()
    {
        transform.DOScale(new Vector3(1.54f, 0.1f, 0.75f), 0.1f);
    }

    private void IncreaseScale()
    {
        transform.DOScale(new Vector3(transform.localScale.x + 1f, transform.localScale.y, transform.localScale.z), 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IStackable>()?.Stack(RunFunctionByCategoryTypeOfBricks);     // tmm
        other.GetComponent<IColorChangable>()?.ChangeColor(ChangeColorCategory);
        other.GetComponent<IObstackle>()?.Hit(()=> {  Death(); Actions.act_playerTouchedToObstackle?.Invoke(); });
        other.GetComponent<IArea>()?.EnterArea(EnteringArea);
    }

    private void EnteringArea(AreaType areaType)
    {
        if (areaType == AreaType.Bonus)
        {
            EnterBonusArea();
        }
        else if (areaType == AreaType.Finish)
        {
            EnterFinishArea();
        }
    }

    private void EnterFinishArea()
    {
        gameManager.SetState(GameStates.InFinishArea);
        canvasManager.OpenMenu("finishPanel");
        Actions.act_enteredFinisArea.Invoke();
    }

    private void EnterBonusArea()
    {
        gameManager.SetState(GameStates.InBonusArea);
        canvasManager.CloseMenu("finishPanel");
        collectedBricks.Clear();
        Actions.act_enteredBonusArea(FinishPanel.instance.Counter);
    }
    private void RunFunctionByCategoryTypeOfBricks(ColorCategory colorCategoryOfCollectingObject, Transform transformOfCollectingObject)
    {
        if (colorCategoryOfCollectingObject == category)
        {

            collectedBricks.Insert(0, transformOfCollectingObject);
            gameManager.IncreaseScore(transformOfCollectingObject.GetChildCount());
            transformOfCollectingObject.position = transform.position + new Vector3(0, 0.1f, 0f);
            Actions.act_trueBrickCollected?.Invoke(transformOfCollectingObject.GetChildCount());

        }
        else
        {
           
                 
            if (collectedBricks.Count <= 0)
            {
                gameManager.SetState(GameStates.InLosePanel);
                canvasManager.OpenMenu("loseGamePanel");
                return;
            }
            
            int destroyedObjectChildCount = collectedBricks[0].GetChildCount();

            Destroy(collectedBricks[0].gameObject,0.1f);    // burada bir tanesi yok oluyot diye action haatsa olabiler
            Destroy(transformOfCollectingObject.gameObject);

            collectedBricks.RemoveAt(0);

            Actions.act_falseBrickCollected?.Invoke(destroyedObjectChildCount);
        }
    }

    private void FallBricks(Vector3 touchedObjectLocalPos)
    {
        float localHeigth = touchedObjectLocalPos.y;

        for (int i = collectedBricks.Count - 1; i >= 0; i--)
        {
            if(collectedBricks[i].localPosition.y > localHeigth)
            {
                Destroy(collectedBricks[i].gameObject, 3f);
                Rigidbody rb = collectedBricks[i].gameObject.AddComponent<Rigidbody>();
                
                rb.AddForce(collectedBricks[i].forward * collectedBricks[i].localPosition.y * 50f + 10 * collectedBricks[i].forward);

                cameraController.SetCameraHeightByCollectedBricks(-collectedBricks[i].GetChildCount());
                collectedBricks.RemoveAt(i);
            }
        }
    }




    private void Update()
    {
        if (collectedBricks.Count > 1 )
        {
            Transform brick;
            if(gameManager.State == GameStates.InRun || gameManager.State == GameStates.InFinishArea)
            {
                for (int i = collectedBricks.Count - 1; i >= 1; i--)
                {
                    brick = collectedBricks[i];
                    brick.position = Vector3.Lerp(brick.transform.position, new Vector3(
                        transform.position.x, 
                        brick.position.y,
                        transform.position.z), collectedBricks.Count / (i * 4f));
                }

                brick = collectedBricks[0];

                brick.position = Vector3.Lerp(brick.position, new Vector3(transform.position.x, brick.position.y, transform.position.z), 1f);
            }
        }
    }




    private void Death()
    {
        canvasManager.OpenMenu("loseGamePanel");
        gameManager.SetState(GameStates.InLosePanel);
    }

    public void ChangeColorCategory(ColorCategory category)
    {

        if (materialManager == null)
        {
            materialManager = MaterialManager.instance;
        }


        if (Time.time - lastChangedColorTime > 1)
        {
            lastChangedColorTime = Time.time;

            this.category = category;
            Color newMaterialColor;

            switch (this.category)
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
            Actions.act_colorChanged?.Invoke(this.category);


        }
    }

    public void ChangeMaterialColor(Color color)
    {
        foreach (MeshRenderer item in pickerParts)
        {
            item.material.color = color;
        }
    }
}