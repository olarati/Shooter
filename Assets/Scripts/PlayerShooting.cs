using UnityEngine;

public class PlayerShooting : CharacterShooting
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletDelay = 0.05f;

    private Transform _bulletSpawnPoint;

    private float _bulletTimer;

    public override void Init()
    {
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
        
        _bulletTimer = 0;
    }

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            _bulletTimer += Time.deltaTime;
            if(_bulletTimer >= _bulletDelay)
            {
                _bulletTimer = 0;
                SpawnBullet();
            }
        }
    }

    private void SpawnBullet()
    {
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
    }
}
