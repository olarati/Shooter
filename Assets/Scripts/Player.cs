using UnityEngine;

public class Player : MonoBehaviour
{
    private const string MovememntHorizontalKey = "Horizontal";
    private const string MovememntVerticalKey = "Vertical";

    private const string IsGroundedKey = "IsGrounded";

    private const string AimingHorizontalKey = "Mouse X";

    [SerializeField] private float _gravityMultiplier = 2f;
    [SerializeField] private float _movementSpeed = 6f;

    [SerializeField] private float _jumpSpeed = 30f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private float _groundCheckExtraUp = 0.2f;

    [SerializeField] private float _aimingSpeed = 130f;

    private Animator _animator;
    private CharacterController _characterController;

    private Vector3 _groundCheckBox;

    private bool _isGrounded;
    private bool _isJumping;
    private float _jumpTimer;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();

        _groundCheckBox = new Vector3(_characterController.radius, 0.0001f, _characterController.radius);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Gravity();
        Movement();
        Jumping();
        Aiming();
    }

    private void Gravity()
    {
        Vector3 gravity = Physics.gravity;
        gravity *= _gravityMultiplier * Time.fixedDeltaTime;
        _characterController.Move(gravity);
    }

    private void Movement()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxis(MovememntHorizontalKey);
        movement.z = Input.GetAxis(MovememntVerticalKey);

        _animator.SetFloat(MovememntHorizontalKey, movement.x);
        _animator.SetFloat(MovememntVerticalKey, movement.z);

        movement *= _movementSpeed * Time.fixedDeltaTime;

        Vector3 relatedMovement = transform.TransformDirection(movement);
        
        _characterController.Move(relatedMovement);
    }

    private void Jumping()
    {
        RefreshIsGrounded();
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isJumping)
        {
            SetIsGrounded(false);
            _isJumping = true;
            _jumpTimer = 0;
        }

        if (_isJumping)
        {
            _jumpTimer += Time.fixedDeltaTime;
            _characterController.Move(Vector3.up * _jumpSpeed * (1 - _jumpTimer / _jumpDuration) * Time.fixedDeltaTime);
            if(_jumpTimer >= _jumpDuration || _isGrounded)
            {
                _isJumping = false; 
            }
        }
    }

    private void RefreshIsGrounded()
    {
        SetIsGrounded(GroundCheck());
    }

    private bool GroundCheck()
    {
        Vector3 startCheckPosition = transform.position + Vector3.up * _groundCheckExtraUp;
        float checkDistance = _groundCheckDistance + _groundCheckExtraUp;
        return Physics.BoxCast(startCheckPosition, _groundCheckBox, Vector3.down, transform.rotation, checkDistance);
    }

    private void SetIsGrounded(bool value)
    {
        if (value != _isGrounded)
        {
            _animator.SetBool(IsGroundedKey, value);
        }
        _isGrounded = value;
    }

    private void Aiming()
    {
        float horizontalRotation = Input.GetAxis(AimingHorizontalKey);
        float nextYEuler = transform.eulerAngles.y + horizontalRotation * _aimingSpeed * Time.fixedDeltaTime;
        transform.eulerAngles = Vector3.up * nextYEuler;
    }

}
