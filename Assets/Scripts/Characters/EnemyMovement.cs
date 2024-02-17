using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : CharacterMovement
{
    private const string MovementHorizontalKey = "Horizontal";
    private const string MovementVerticalKey = "Vertical";

    protected NavMeshAgent _navMeshAgent;
    protected Transform _playerTransform;

    private Animator _animator;
    private Vector3 _prevPosition;

    protected abstract void Movement();

    protected override void OnInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _playerTransform = FindAnyObjectByType<Player>().transform;

        _prevPosition = transform.position;
    }

    protected override void OnStop()
    {
        _navMeshAgent.enabled = false;
        RefreshAnimation();
    }

    protected void MoveToPlayer()
    {
        SetTargetPosition(_playerTransform.position);
    }

    protected void SetTargetPosition(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        Movement();
        RefreshAnimation();
    }


    private void RefreshAnimation()
    {
        Vector3 curPosition = transform.position;
        Vector3 deltaMove = curPosition - _prevPosition;
        _prevPosition = curPosition;

        deltaMove.Normalize();

        float relatedX = Vector3.Dot(deltaMove, transform.right);
        float relatedY = Vector3.Dot(deltaMove, transform.forward);

        _animator.SetFloat(MovementHorizontalKey, relatedX);
        _animator.SetFloat(MovementVerticalKey, relatedY);
    }
}
