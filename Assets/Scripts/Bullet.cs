using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private LayerMask _hitLayerMask;
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _lifeTime = 2f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * _speed;
    }

    private void Update()
    {
        ReduceLifeTime();
    }

    private void ReduceLifeTime()
    {
        _lifeTime -= Time.deltaTime;
        if(_lifeTime <= 0)
        {
            DestroyBullet();
        }
    }

    private void FixedUpdate()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _speed * Time.fixedDeltaTime, _hitLayerMask))
        {
            Hit(hit);
        }
    }

    private void Hit(RaycastHit hit)
    {
        Instantiate(_hitPrefab, hit.point, Quaternion.LookRotation(-transform.up, -transform.forward));
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
