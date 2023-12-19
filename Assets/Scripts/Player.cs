using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private const string MovememntHorizontalKey = "Horizontal";
    private const string MovememntVerticalKey = "Vertical";

    [SerializeField] private float _gravityMultiplier = 2f;
    [SerializeField] private float _movementSpeed = 6f;
    [SerializeField] private float _jumpSpeed = 30f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _groundCheckDistance = 0.2f;

    private Animator _animator;
    private CharacterController _characterController;

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
    }

    private void FixedUpdate()
    {
        Gravity();
        Movement();
        RefreshIsGrounded();
        Jumping();
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
            _isGrounded = false;
            _isJumping = true;
            _jumpTimer = 0;
        }

        if (_isJumping)
        {
            _jumpTimer += Time.fixedDeltaTime;
            _characterController.Move(Vector3.up * _jumpSpeed * (1 - _jumpTimer / _jumpDuration) * Time.fixedDeltaTime);
            if(_jumpTimer >= _jumpDuration)
            {
                _isJumping = false; 
            }
            if (_isGrounded)
            {
                _isJumping = false;
            }
        }
    }

    private void RefreshIsGrounded()
    {
        _isGrounded = GroundCheck();
    }

    bool GroundCheck()
    {
        Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance);
    }

}
