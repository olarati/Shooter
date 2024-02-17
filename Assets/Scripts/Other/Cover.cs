using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] private float _extraOffset = 0.7f;
    private Collider _collider;

    public Vector3 GetOppositePosition(Vector3 targetPosition)
    {
        Vector3 delta = targetPosition - transform.position;
        Vector3 oppositePosition = transform.position - delta;
        oppositePosition = _collider.bounds.ClosestPoint(oppositePosition);
        oppositePosition -= delta.normalized * _extraOffset;
        return oppositePosition;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _collider = GetComponentInChildren<Collider>();
    }
}
