using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript
{
    private float _hitRadius;

    public ExplodeScript(float hitRadius)
    {
        _hitRadius = hitRadius;
    }

    public void Explode(Vector3 explodePosition)
    {
        Collider[] colliders =
            Physics.OverlapSphere(explodePosition, _hitRadius);

        foreach (Collider collider in colliders)
        {
            GameObject gameObject = collider.gameObject;

            Debug.Log(gameObject.tag);

            Rigidbody targetRb;
            if (gameObject.TryGetComponent<Rigidbody>(out targetRb))
                targetRb.AddExplosionForce(1000f, explodePosition, _hitRadius);
            
            ReactiveTarget reaction;
            if (gameObject.TryGetComponent<ReactiveTarget>(out reaction))
                reaction.ReactToHit();
        }
    }

}
