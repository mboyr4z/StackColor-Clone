using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gold : MonoBehaviour, ICollectable, IDestroyable
{
    [SerializeField] private CollectableTypes collectableTypes;

    [SerializeField] private float liveTimeThanCollecting;

    public void Collect()
    {
        
        GameManager.instance.IncreaseCoin(1);

        Destroy(gameObject.GetComponent<Rigidbody>());
        GetComponent<BoxCollider>().enabled = false;

        transform.DOMove(new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z + 25f),liveTimeThanCollecting).SetEase(Ease.InCirc);
        DestoryObject();

        Actions.act_goldCollected?.Invoke();
    }

    public void DestoryObject()
    {
        Destroy(gameObject, liveTimeThanCollecting + 0.1f);
    }
}
