using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementRunThanHide : EnemyMovement
{
    [SerializeField] private float _searchRange = 10f;
    [SerializeField] private LayerMask _coverLayerMask;

    private Vector3 _hidePosition;
    private bool _isCoverFound;

    protected override void Movement()
    {
        if (!_isCoverFound)
        {
            MoveToPlayer();
            FindCover();
        }
        else
        {
            SetTargetPosition(_hidePosition);
        }
    }

    private void FindCover()
    {
        Cover cover = GetRandomCover();
        if (!cover)
        {
            return;
        }
        _isCoverFound = true;
        _hidePosition = cover.GetOppositePosition(_playerTransform.position);
    }

    private Cover GetRandomCover()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, _searchRange, _coverLayerMask);
        if (collidersInRange == null || collidersInRange.Length == 0)
        {
            return null;
        }

        Collider randomCollider = collidersInRange[Random.Range(0,collidersInRange.Length)];
        Cover randomCover = randomCollider.GetComponentInParent<Cover>();
        return randomCover;
    }
}
