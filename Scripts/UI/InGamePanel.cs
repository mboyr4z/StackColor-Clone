using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGamePanel : MonoBehaviour
{
    [SerializeField] private Slider movementSlider;

    [SerializeField] private Text totalCollectedBrickCountText;

    [SerializeField] private GameObject increasePrefab;

    [SerializeField] private Text totalCoinText;

    [SerializeField] private Transform increaseTextStartPos;

    [SerializeField] private Transform player;



    private float startDistanceBetweenStartAndFinish;

    private GameManager gameManager;

    private FinishArea finishArea;

    private void OnEnable()
    {

        if(finishArea == null)
        {
            finishArea = FinishArea.instance;
        }

        if(gameManager == null)
        {
            gameManager = GameManager.instance;
        }
        SetTotalCoinText();
        startDistanceBetweenStartAndFinish = Vector3.Distance(player.position, finishArea.GetPosition());
    }

    private void Start()
    {
        Actions.act_trueBrickCollected += CreateIncreasePrefab;
        Actions.act_trueBrickCollected += UpdateScoreText;
        Actions.act_goldCollected += SetTotalCoinText;
    }

    private void OnDestroy()
    {
        Actions.act_trueBrickCollected -= CreateIncreasePrefab;
        Actions.act_trueBrickCollected -= UpdateScoreText;
        Actions.act_goldCollected -= SetTotalCoinText;
    }

    private void SetSlider(float value)
    {
        movementSlider.value = value;
    }

    private void FixedUpdate()
    {
        float distanceRightNow = Vector3.Distance(player.position, finishArea.GetPosition());
        float distanceRate = 1- (distanceRightNow / startDistanceBetweenStartAndFinish);
        SetSlider(distanceRate);
    }



    private void CreateIncreasePrefab(int childCountOfCollectedBrick)
    {
        GameObject createdIncreaseText = Instantiate(increasePrefab, new Vector3(0,0,0f), Quaternion.identity);
        createdIncreaseText.transform.parent = transform;
        createdIncreaseText.transform.position = increaseTextStartPos.position;

        createdIncreaseText.transform.DOMove(createdIncreaseText.transform.position + new Vector3(0,100f,0), 1f).OnComplete(() => {
            Destroy(createdIncreaseText);
        });

        createdIncreaseText.GetComponent<Text>().text = "+"+childCountOfCollectedBrick;

        createdIncreaseText.GetComponent<Text>().DOColor(new Color(255, 255, 255, 0),0.9f);
    }

    private void UpdateScoreText(int s)
    {
        totalCollectedBrickCountText.text = gameManager.GetScore().ToString();
    }

    private void OnApplicationQuit()
    {
        DOTween.KillAll();
    }

    private void SetTotalCoinText()
    {
        totalCoinText.text = gameManager.GetTotalCoin().ToString();
    }
}
