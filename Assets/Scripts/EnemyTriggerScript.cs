using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    GameObject _enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        _enemy = Instantiate(_enemyPrefab) as GameObject;

    }

}
