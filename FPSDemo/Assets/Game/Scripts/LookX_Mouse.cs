using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX_Mouse : MonoBehaviour
{
    private float _lookX = 0.0f;
    private Vector3 _newRotation = new Vector3();

    [SerializeField]
    private float _lookSensitivity = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lookX = Input.GetAxis("Mouse X") * _lookSensitivity;

        //Euler Angles used for rotation
        _newRotation = this.transform.localEulerAngles;
        _newRotation.y += _lookX;

        this.transform.localEulerAngles = _newRotation;
    }
}
