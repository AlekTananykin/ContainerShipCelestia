using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSetting : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject mine = Instantiate(_bombPrefab) as GameObject;
            mine.transform.position = 
                transform.position + transform.forward;

            Debug.Log(transform.position);

            Rigidbody mineRb = mine.GetComponent<Rigidbody>();
            mineRb.AddForce(this.transform.forward, ForceMode.Impulse);
        }
    }
}
