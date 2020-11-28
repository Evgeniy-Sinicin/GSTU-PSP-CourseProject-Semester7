using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private Rigidbody _rb;

    private Vector3 _moveDirection;
    private Vector3 _playerRotationDirection;
    private Vector3 _cameraRotationDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Called with an interval of 0.02 seconds
    /// </summary>
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (_moveDirection != Vector3.zero)
        {
            // Moves an object until it collides
            _rb.MovePosition(_rb.position + _moveDirection * Time.fixedDeltaTime);
        }
    }

    //private void OnCollisionStay()
    //{
    //    if ()
    //}

    private void Rotate()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_playerRotationDirection));
        _camera?.transform.Rotate(-_cameraRotationDirection);
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public void Rotate(Vector3 rotation)
    {
        _playerRotationDirection = rotation;
    }

    public void RotateCamera(Vector3 rotation)
    {
        _cameraRotationDirection = rotation;
    }

    public void Jump(float jumpForce)
    {
        _rb.AddForce(new Vector3(0f, 2f, 0f) * jumpForce, ForceMode.Impulse);
    }
}
