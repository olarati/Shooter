using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _lifeTime = 2f;

    private int _damage;

    public void SetDamage(int value)
    {
        _damage = value;
    }

    private void Update()
    {
        ReduceLifeTime();
        CheckHit();
        Move();
    }

    private void ReduceLifeTime()
    {
        _lifeTime -= Time.deltaTime;
        if(_lifeTime <= 0)
        {
            DestroyBullet();
        }
    }

    private void CheckHit()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _speed * Time.deltaTime)
            && !hit.collider.isTrigger)
        {
            Hit(hit);
        }
    }

    private void Hit(RaycastHit hit)
    {
        CheckCharacterHit(hit);
        CheckPhysicObjectHit(hit);
        Instantiate(_hitPrefab, hit.point, Quaternion.LookRotation(-transform.up, -transform.forward));
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void CheckCharacterHit(RaycastHit hit)
    {
        CharacterHealth hitedHealth = hit.collider.GetComponentInChildren<CharacterHealth>();
        if (hitedHealth)
        {
            hitedHealth.AddHealthPoints(-_damage);
        }
    }

    private void CheckPhysicObjectHit(RaycastHit hit)
    {
        IPhysicHitable hitedPhysicObject = hit.collider.GetComponentInParent<IPhysicHitable>();
        if (hitedPhysicObject != null)
        {
            hitedPhysicObject.Hit(transform.forward * _speed, hit.point);
        }
    }
}
