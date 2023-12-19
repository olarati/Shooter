using UnityEngine;

public class Player : MonoBehaviour
{
    private const string MovememntHorizontalKey = "Horizontal";
    private const string MovememntVerticalKey = "Vertical";

    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _jumpForce = 100f;

    private Animator _animator;
    private CharacterController _characterController;

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
        Jumping();
    }

    private void Gravity()
    {
        Vector3 gravity = Physics.gravity;
        gravity *= Time.deltaTime;
        _characterController.Move(gravity);
    }

    private void Movement()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxis(MovememntHorizontalKey);
        movement.z = Input.GetAxis(MovememntVerticalKey);

        _animator.SetFloat(MovememntHorizontalKey, movement.x);
        _animator.SetFloat(MovememntVerticalKey, movement.z);

        movement *= _movementSpeed * Time.deltaTime;

        Vector3 relatedMovement = transform.TransformDirection(movement);
        
        _characterController.Move(relatedMovement);
    }

    private void Jumping()
    {
        if (!_characterController.isGrounded)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           // _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

}
