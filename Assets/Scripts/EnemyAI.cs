using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool _isAlive;
    [SerializeField] private float _speed = 0.4f;
    public float obstacleRange = 5.0f;

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

        transform.Translate(0, 0, _speed * Time.deltaTime, Space.Self);

        Ray ray = new Ray(transform.position + new Vector3(0, 0.4f), transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.3f, out hit))
        {
            if (hit.collider.gameObject.tag == "Bomb")
                return;

            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-60, 60);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void SetAlive(bool isAlive)
    {
        this._isAlive = isAlive;
    }
}
