using UnityEngine;

public class EnemyShootingAllAtOnce : CharacterShooting
{
    [SerializeField] private float _shootingRange = 10f;

    private Transform _targetTransform;
    private bool _isInRange;

    protected override void OnInit()
    {
        base.OnInit();
        _targetTransform = FindAnyObjectByType<Player>().transform;
    }

    protected override void Shooting()
    {
        if(!_isInRange && CheckTargetInRange())
        {
            _isInRange = true;
        }

        if (_isInRange)
        {
            if (CheckHasBulletsInRow())
            {
                Shoot();
            }
            else
            {
                _isInRange = false;
            }
        }
    }

    protected override void Reloading()
    {
        if (!CheckHasBulletsInRow())
        {
            Reload();
        }
    }

    private bool CheckTargetInRange()
    {
        return (_targetTransform.position - transform.position).magnitude <= _shootingRange;
    }

}
