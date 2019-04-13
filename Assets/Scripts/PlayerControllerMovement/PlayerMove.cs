using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string _horizontalInputName;
    [SerializeField] private string _verticalInputName;

    public float MovementSpeed;
    public bool freeLook;
    
    private CharacterController _characterController;
    private float _speed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _speed = MovementSpeed;
    }

    private void Update()
    {
        SprintMovement();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        var horizontalInput = Input.GetAxisRaw(_horizontalInputName) * _speed;
        var verticalInput = Input.GetAxisRaw(_verticalInputName) * _speed;

        var forwardMovement = transform.forward * verticalInput;
        if (freeLook) forwardMovement = transform.GetChild(0).forward * verticalInput;
        var rightMovement = transform.right * horizontalInput;

        _characterController.Move((forwardMovement + rightMovement) * Time.deltaTime);
    }

    private void SprintMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _speed = MovementSpeed + 20;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            _speed = MovementSpeed;
    }
}
