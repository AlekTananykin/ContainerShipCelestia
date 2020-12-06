using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMoving : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    public GameObject _shooter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Debug.Log("Player hit");

        if (null != _shooter)
        {
            EnemyAI enemyAi = _shooter.GetComponent<EnemyAI>();
            if (null != enemyAi)
                enemyAi.Recharge();
        }

        Destroy(this.gameObject);
    }
}
