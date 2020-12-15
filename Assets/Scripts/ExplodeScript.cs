using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript
{
    private float _hitRadius;
    private float _explosionForce;

    public ExplodeScript(float hitRadius, float explosionForce)
    {
        _hitRadius = hitRadius;
        _explosionForce = explosionForce;
    }

    public void Explode(Vector3 explodePosition)
    {
        Collider[] colliders =
            Physics.OverlapSphere(explodePosition, _hitRadius);
        const float lift = 0;
        foreach (Collider item in colliders)
        {
            GameObject gameObject = item.gameObject;

            Debug.Log(gameObject.name);

            Rigidbody targetRb;
            if (gameObject.TryGetComponent(out targetRb))
                targetRb.AddExplosionForce(_explosionForce, explodePosition, 
                    _hitRadius, lift, ForceMode.Impulse);

            IReactToHit reaction;
            float ditance = (gameObject.transform.position - explodePosition).sqrMagnitude;
            if (gameObject.TryGetComponent(out reaction))
            {
                reaction.ReactToHit((int)((0 == ditance)? _explosionForce: 
                    (_explosionForce / ditance)));
            }
        }
    }

}
