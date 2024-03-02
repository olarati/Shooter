using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private AudioClip _humanHitClip;
    [SerializeField] private AudioClip _commonHitClip;

    private int _damage;
    private bool _isHumanHit;

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
        GameObject hitSample = Instantiate(_hitPrefab, hit.point, Quaternion.LookRotation(-transform.up, -transform.forward));
        PlaySound(hitSample, _isHumanHit);
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
            _isHumanHit = true;
        }
    }

    private void CheckPhysicObjectHit(RaycastHit hit)
    {
        IPhysicHittable hittedPhysicObject = hit.collider.GetComponentInParent<IPhysicHittable>();
        if (hittedPhysicObject != null)
        {
            hittedPhysicObject.Hit(transform.forward * _speed, hit.point);
        }
    }

    private void PlaySound(GameObject hit, bool isHumanHit)
    {
        AudioSource audioSource = hit.GetComponentInChildren<AudioSource>();
        audioSource.PlayOneShot(isHumanHit ? _humanHitClip : _commonHitClip);
    }
}
