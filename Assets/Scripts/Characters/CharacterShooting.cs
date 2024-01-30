using UnityEngine;

public abstract class CharacterShooting : CharacterPart
{
    private const float DefaultDamageMutiplier = 1;

    private Weapon _weapon;

    private float _damageMultiplier = DefaultDamageMutiplier;
    private float _damageMultiplierDuration;
    private float _damageMultiplierTimer;

    public void SetDamageMultiplier(float multiplier, float duration)
    {
        _damageMultiplier = multiplier;
        _damageMultiplierDuration = duration;
        _damageMultiplierTimer = 0;
    }

    protected override void OnInit()
    {
        _weapon = GetComponentInChildren<Weapon>();
        SetDefaultDamageMultiplier();
    }

    protected void DamageBonusing()
    {
        if(_damageMultiplierDuration <= 0)
        {
            return;
        }

        _damageMultiplierTimer += Time.deltaTime;
        if(_damageMultiplierTimer >= _damageMultiplierDuration)
        {
            SetDefaultDamageMultiplier();
        }
    }

    protected void SpawnBullet(Bullet prefab, Transform spawnPoint)
    {
        Bullet bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        InitBullet(bullet);
    }

    private void SetDefaultDamageMultiplier()
    {
        SetDamageMultiplier(DefaultDamageMutiplier, 0);
    }

    private void InitBullet(Bullet bullet)
    {
        bullet.SetDamage((int) (_weapon.Damage * _damageMultiplier));
    }
}
