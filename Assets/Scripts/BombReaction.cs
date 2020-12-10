﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombReaction : MonoBehaviour
{
    private const float _hitRadius = 5f;
    private ExplodeScript _explode;

    BombReaction() => _explode = new ExplodeScript(_hitRadius);

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (!collisionObject.CompareTag("Enemy"))
            return;

        _explode.Explode(transform.position);

        Destroy(this.gameObject);
    }
}

