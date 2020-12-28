using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeReaction : MonoBehaviour
{

    [SerializeField] private ParticleSystem _burstPrefab;
    private ParticleSystem _burst;

    [SerializeField] private AudioClip _burstSound;
    [SerializeField] private AudioClip _impactSound;
    private AudioSource _audioSource;

    private float _timeToExplosion = 5f;
    private bool _isActive = false;
    ExplodeScript _explode;

    private bool _isBurstBegun = false;

    private const float _hitRadius = 2f;
    private const float _explosionForce = 1000f;
    GranadeReaction() => _explode = new ExplodeScript(_hitRadius, _explosionForce);

    public void Awake()
    {
        _burst = Instantiate(_burstPrefab);

        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;

        _timeToExplosion -= Time.deltaTime;

        if (_timeToExplosion > 0)
            return;

        if (_isBurstBegun)
            return;

        _audioSource.clip = _burstSound;
        _audioSource.Play();
        _isBurstBegun = true;

        _burst.transform.position = transform.position;
        _burst.Play();

        _explode.Explode(transform.position);

        Destroy(this.gameObject, 1f);
    }

    public void Activate()
    {
        _isActive = true;
        _audioSource.clip = _impactSound;
        _audioSource.loop = false;
        _audioSource.Play();
    }
}
