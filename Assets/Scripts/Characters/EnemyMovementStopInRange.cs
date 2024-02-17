using UnityEngine;

public class EnemyMovementStopInRangent : EnemyMovement
{
    [SerializeField] private float _minRange = 5f;

    protected override void Movement()
    {
        if (CheckTargetInRange())
        {
            _navMeshAgent.stoppingDistance = 5f;
        }
        else
        {
            MoveToPlayer();
        }
    }
    private bool CheckTargetInRange()
    {
        return (_playerTransform.position - transform.position).magnitude <= _minRange;
    }
}
