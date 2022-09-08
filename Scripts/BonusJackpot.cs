using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BonusJackpot : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject bonusPointPrefab;

    [SerializeField] private Transform parentCanvas;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IStackable>()?.Stack((ColorCategory category, Transform touchedBrick) => { CreateBonusText(touchedBrick.GetChildCount()); });
    }

    private void CreateBonusText(int childCountOfTouchedBrick)
    {
        GameObject createdBonusPoint = Instantiate(bonusPointPrefab , parentCanvas);
        createdBonusPoint.GetComponent<Text>().text = "+" + childCountOfTouchedBrick;
        createdBonusPoint.GetComponent<Text>().DOColor(new Color(255, 255, 255, 0), 2f);
        createdBonusPoint.transform.DOLocalMoveY(createdBonusPoint.transform.position.y + 3f, 2f).OnComplete(() =>
        {
           Destroy(createdBonusPoint.gameObject);
        });

    }
}
