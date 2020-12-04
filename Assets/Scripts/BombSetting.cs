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
            RaycastHit hit;
            Ray ray = new Ray(transform.position + 
                transform.forward + transform.up, new Vector3(0, -1, 0));

            if (Physics.Raycast(ray, out hit))
            {
                GameObject mine = Instantiate(_bombPrefab) as GameObject;
                mine.transform.position = hit.point;
            }
        }
    }
}
