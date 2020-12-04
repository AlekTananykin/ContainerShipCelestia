using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float _sensHor = 9.0f;
    public float _sensVert = 9.0f;

    public float _minVert = -45.0f;
    public float _maxVert = 45.0f;
    private float _rotationX = 0;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (null != body)
            body.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _sensVert;
        _rotationX = Mathf.Clamp(_rotationX, _minVert, _maxVert);

        float delta = Input.GetAxis("Mouse X") * _sensHor;
        float rotationY = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
}
