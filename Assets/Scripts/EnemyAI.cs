using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool _isAlive;
    [SerializeField] private float _speed = 0.4f;
    public float obstacleRange = 0.5f;
    [SerializeField] private GameObject _fireballPrefab = null;
    private GameObject _fireball = null;
    private GameObject _player = null;


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
                ShootThePlayer();
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
            ShootThePlayer();
            return;
        }

        if (hit.distance < obstacleRange)
        {
            float angle = Random.Range(-60, 60);
            transform.Rotate(0, angle, 0);
        }
        transform.Translate(0, 0, _speed * Time.deltaTime, Space.Self);
    }

    private void ToFollowThePlayer()
    {
        Vector3 targetPos = new Vector3(_player.transform.position.x, 
            this.transform.position.y, 
            _player.transform.position.z);
        this.transform.LookAt(targetPos);
    }

    private void ShootThePlayer()
    {
        if (null != _fireball || null == _player)
            return;

        _fireball = Instantiate(_fireballPrefab);
        _fireball.transform.position =
           transform.position + new Vector3(0, 1, 0) + transform.forward * 1.5f;

        _fireball.transform.LookAt(_player.transform.position);
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

}
