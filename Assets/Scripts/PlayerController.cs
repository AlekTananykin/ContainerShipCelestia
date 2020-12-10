using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _helth;

    void Start()
    {
        _helth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToHit(float hitAccount)
    {
        _helth -= hitAccount;
        Debug.Log("Player hit. " + _helth.ToString());
    }
}
