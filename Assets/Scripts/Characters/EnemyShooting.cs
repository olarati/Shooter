using UnityEngine;

public abstract class EnemyShooting : CharacterShooting
{
    [SerializeField] private float _shootingRange = 10f;

    private Transform _targetTransform;

    protected override void OnInit()
    {
        base.OnInit();
        _targetTransform = FindAnyObjectByType<Player>().transform;
    }

    protected override void Reloading()
    {
        if (!CheckHasBulletsInRow())
        {
            Reload();
        }
    }

    protected bool CheckTargetInRange()
    {
        return (_targetTransform.position - transform.position).magnitude <= _shootingRange;
    }

}
