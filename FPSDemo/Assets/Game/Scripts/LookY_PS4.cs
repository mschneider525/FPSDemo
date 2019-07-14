using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY_PS4 : MonoBehaviour
{
    private float _lookY = 0.0f;
    private Vector3 _newRotation = new Vector3();

    [SerializeField]
    private float _lookSensitivity = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lookY = Input.GetAxis("Look Y") * _lookSensitivity;

        //Euler Angles used for rotation
        _newRotation = this.transform.localEulerAngles;
        _newRotation.x += _lookY;

        //Look Up, but not too much cuz weird stuff happens
        if (_newRotation.x < 275.0f && _newRotation.x > 180.0f)
        {
            if (_newRotation.x < 275.0f)
            {
                _newRotation.x = 275.0f;
            }
        }

        //Look Down, but not too much cuz weird stuff happens
        if (_newRotation.x > 85.0f && _newRotation.x < 180.0f)
        {
            if (_newRotation.x > 85.0f)
            {
                _newRotation.x = 85.0f;
            }
        }

        this.transform.localEulerAngles = _newRotation;
    }
}
