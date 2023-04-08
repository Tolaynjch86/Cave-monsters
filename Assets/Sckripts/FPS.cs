using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private CharacterController _characterController;
    public float speed = 5;
    private Camera _camera;
    public float mouseSens = 350;
    private float _xRotation;
    private float _g = 9.81f;
    private Vector3 _velocity;
    public Transform groundCheck;
    private float _checkRadius = 0.4f;
    public LayerMask groundMask;
    private bool _isGrounded;
    public float jumpHeight = 3f;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Cam();
        Jump();
    }
    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = Mathf.Sqrt(2 * _g * jumpHeight);
        }
    }

    private void Cam()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -40, 30);
        _camera.transform.localRotation = Quaternion.Euler(Vector3.right * _xRotation);
    }
    private void Move()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, _checkRadius, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }

        float directionX = Input.GetAxis("Horizontal");
        float directionZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * directionX + transform.forward * directionZ;
        _characterController.Move(move * speed * Time.deltaTime);
        _velocity.y -= _g * 2 * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
