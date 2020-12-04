using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombReaction : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (!collisionObject.CompareTag("Enemy"))
            return;
        
        ReactiveTarget reactiveTarget = 
            collisionObject.GetComponent<ReactiveTarget>();
        if (null != reactiveTarget)
            reactiveTarget.ReactToHit();

        Destroy(this.gameObject);
    }
}
