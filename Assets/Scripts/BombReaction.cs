using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombReaction : MonoBehaviour
{
    private const float _hitRadius = 5f;
    private const float _explosionForce = 10000f;
    private ExplodeScript _explode;

    BombReaction() => _explode = new ExplodeScript(_hitRadius, _explosionForce);

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (!collisionObject.CompareTag("Enemy"))
            return;

        _explode.Explode(transform.position);
        Destroy(this.gameObject);
    }
}

