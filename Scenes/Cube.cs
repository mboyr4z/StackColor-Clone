using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    /* Obj obj;

     Transform target;

     public float sabit;

     private void Start()
     {
         obj = Obj.instance;
         target = obj.transform;
     }

     private void LateUpdate()
     {
         transform.position = Vector3.Lerp(transform.position,
             new Vector3(target.position.x, transform.position.y, target.position.z),0.1f - transform.position.y * sabit );
     }*/

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * transform.position.y * 80);
    }
}
