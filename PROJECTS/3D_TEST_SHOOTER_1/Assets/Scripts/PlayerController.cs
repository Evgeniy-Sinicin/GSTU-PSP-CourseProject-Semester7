using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _lookSpeed = 5f;
    [SerializeField]
    private float _jumpForce = 5;

    private bool _isGrounded;
    private PlayerMotor _pm;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pm = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    private void OnCollisionStay()
    {
        _isGrounded = true;
    }

    private void OnCollisionExit()
    {
        _isGrounded = false;
    }

    private void Move()
    {
        var dx = Input.GetAxisRaw("Horizontal");
        //var dy = Input.GetAxisRaw("Jump");
        var dz = Input.GetAxisRaw("Vertical");

        var x = transform.right * dx;
        //var y = transform.up * dy;
        var z = transform.forward * dz;

        //var direction = (x + y + z).normalized * _speed;
        var direction = (x + z).normalized * _speed;

        _pm.Move(direction);
    }

    private void Rotate()
    {
        var dx = Input.GetAxisRaw("Mouse Y");
        var dy = Input.GetAxisRaw("Mouse X");

        var cameraRotationX = new Vector3(dx, 0f, 0f) * _lookSpeed; 
        var playerRotationY = new Vector3(0f, dy, 0f) * _lookSpeed;

        _pm.RotateCamera(cameraRotationX);
        _pm.Rotate(playerRotationY);
    }

    private void Jump()
    {
        //if (_isGrounded)
        //{
        //    transform.Translate(0f, _jumpForce * Input.GetAxis("Jump") * Time.fixedDeltaTime, 0f);
        //}

        if (Input.GetKeyDown(KeyCode.Space) &&
            _isGrounded)
        {
            _pm.Jump(_jumpForce);
        }
    }
}
