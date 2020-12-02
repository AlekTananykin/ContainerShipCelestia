using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool _isAlive;
    public float _speed = 0.1f;
    public float obstacleRange = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAlive)
            return;

        transform.Translate(0, 0, _speed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
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
