using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombReaction : MonoBehaviour
{
    [SerializeField] private ParticleSystem _burstPrefab;
    private ParticleSystem _burst;

    private const float _hitRadius = 5f;
    private const float _explosionForce = 10000f;
    private ExplodeScript _explode;

    BombReaction() => _explode = new ExplodeScript(_hitRadius, _explosionForce);

    public void Awake()
    {
        _burst = Instantiate(_burstPrefab);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (!collisionObject.CompareTag("Enemy"))
            return;

        _burst.transform.position = transform.position;
        _burst.Play();
        
        _explode.Explode(transform.position);

        Destroy(this.gameObject, 0.5f);
    }
}

