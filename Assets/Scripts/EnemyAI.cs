using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool _isAlive;
    private const float _speed = 5.0f;
    public float _obstacleRange = 0.5f;
    private const float _shootDistance = 5.0f;

    [SerializeField] private GameObject _fireballPrefab = null;
    private GameObject _fireball = null;
    private GameObject _player = null;

    private const float _rayRechargeTimeMs = 1.5f;
    private float _rayTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (null != body)
            body.freezeRotation = true;

        _isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAlive)
            return;

        RaycastHit hit;
        GameObject hitObject;
        
        if (null != _player)
        {
            ToFollowThePlayer();
            hitObject = GetHitObject(out hit);

            if (hitObject.CompareTag("Player") || 
                hitObject.CompareTag("RoboFireBall"))
            {
                ReactOnPlayer(hit.distance);
                return;
            }
            _player = null;
        }
        else
            hitObject = GetHitObject(out hit);

        if (null == hitObject)
            return;

        if (hitObject.CompareTag("Player"))
        {
            _player = hitObject;
            ToFollowThePlayer();
            ReactOnPlayer(hit.distance);
            return;
        }

        if (hit.distance < _obstacleRange)
        {
            float angle = Random.Range(-60, 60);
            transform.Rotate(0, angle, 0);
        }
        transform.Translate(0, 0, _speed * Time.deltaTime, Space.Self);
    }

    private void ReactOnPlayer(float diatance)
    {
        if (diatance > _shootDistance)
            transform.Translate(0, 0, _speed * Time.deltaTime, Space.Self);
        else
            ShootThePlayer();
    }

    private void ToFollowThePlayer()
    {
        Vector3 targetPos = new Vector3(_player.transform.position.x, 
            this.transform.position.y, 
            _player.transform.position.z);
        this.transform.LookAt(targetPos);
    }

    private GameObject GetHitObject(out RaycastHit hit)
    {
        if (Physics.SphereCast(GetRay(), 0.5f, out hit))
            return hit.transform.gameObject;

        return null;
    }

    private Ray GetRay()
    {
        return new Ray(transform.position + new Vector3(0, 0.7f, 0),
            transform.forward);
    }

    public void SetAlive(bool isAlive)
    {
        this._isAlive = isAlive;
    }

    public void Recharge()
    {
        _fireball = null;
    }

    private void ShootThePlayer()
    {
        ShootThePlayerByRay();
        //ShootThePlayerByFireBall();
    }

    private void ShootThePlayerByRay()
    {
        if (null == _player)
            return;

        _rayTime += Time.deltaTime;
        if (_rayTime < _rayRechargeTimeMs)
            return;

        _rayTime = 0;
        Debug.DrawRay(transform.position, 
            _player.transform.position, Color.red, 1400f);

        var playerController = _player.GetComponent<PlayerController>();
        playerController.ReactToHit(5);
    }

    private void ShootThePlayerByFireBall()
    {
        if (null != _fireball || null == _player)
            return;

        _fireball = Instantiate(_fireballPrefab);
        _fireball.transform.position =
           transform.position + new Vector3(0, 1, 0) + transform.forward * 1.5f;

        _fireball.transform.LookAt(_player.transform.position);
    }
}
