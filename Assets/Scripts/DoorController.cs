using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IDevice
{
    private Animator _animator;
    private BoxCollider _collider;
    private bool _isDoorOpen = false;

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = _isDoorOpen;

        _animator = GetComponent<Animator>();
        _animator.SetBool("character_nearby", _collider.isTrigger);
    }

    public void Operate(string key)
    {
        _isDoorOpen = !_isDoorOpen;
        OperatetheDoor();
    }

    void OperatetheDoor()
    {
        _collider.isTrigger = _isDoorOpen;
        _animator.SetBool("character_nearby", _isDoorOpen);

        if (_collider.isTrigger)
            _collider.size =
                new Vector3(_collider.size.x, _collider.size.y, 1f);
        else
            _collider.size =
                new Vector3(_collider.size.x, _collider.size.y, 0.1f);
    }

    public void OnTriggerExit(Collider other)
    {
        _isDoorOpen = false;
        OperatetheDoor();
    }
}
