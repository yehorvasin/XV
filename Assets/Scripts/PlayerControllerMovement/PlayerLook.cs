using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string _mouseXInputName;
    [SerializeField] private string _mouseYInputName;
    [SerializeField] private float _mouseSensitivity;

    private float _xAxisClamp;
    private float _yAxisClamp;
    
    private void Awake()
    {
        _xAxisClamp = 0;
        _yAxisClamp = 0;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        var mouseX = Input.GetAxisRaw(_mouseXInputName) * _mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxisRaw(_mouseYInputName) * _mouseSensitivity * Time.deltaTime;

        _xAxisClamp += mouseY;
        _yAxisClamp += mouseX;

        if (_xAxisClamp > 90f)
        {
            _xAxisClamp = 90;
            mouseY = 0;
            ClampXAxisRotationToValue(270);
        }
        else if (_xAxisClamp < -90)
        {
            _xAxisClamp = -90;
            mouseY = 0;
            ClampXAxisRotationToValue(90);
        }

        transform.Rotate(Vector3.left * mouseY);
        transform.parent.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        var eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
    
    private void ClampYAxisRotationToValue(float value)
    {
        var eulerRotation = transform.parent.eulerAngles;
        eulerRotation.y = value;
        transform.parent.eulerAngles = eulerRotation;
    }
}
