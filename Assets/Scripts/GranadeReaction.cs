using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeReaction : MonoBehaviour
{

    [SerializeField] private ParticleSystem _burstPrefab;
    private ParticleSystem _burst;

    private float _timeToExplosion = 5f;
    private bool _isActive = false;
    ExplodeScript _explode;

    private const float _hitRadius = 2f;
    private const float _explosionForce = 1000f;
    GranadeReaction() => _explode = new ExplodeScript(_hitRadius, _explosionForce);

    public void Awake()
    {
        _burst = Instantiate(_burstPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;

        _timeToExplosion -= Time.deltaTime;

        if (_timeToExplosion > 0)
            return;

        _burst.Play();
        _explode.Explode(transform.position);

        Destroy(this.gameObject, 1f);

    }

    public void Activate()
    {
        _isActive = true;
    }
}
