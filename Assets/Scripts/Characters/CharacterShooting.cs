using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public abstract class CharacterShooting : CharacterPart, IWeaponDependent
{
    public const float DefaultDamageMutiplier = 1;
    private const string WeaponIdKey = "WeaponId";
    private const string IsReloadingKey = "IsReloading";

    private Animator _animator;
    private Rig _rig;
    private Weapon[] _weapons;
    private Weapon _currentWeapon;
    private WeaponIdentity _weaponId;

    private float _damageMultiplier = DefaultDamageMutiplier;
    private float _damageMultiplierDuration;
    private float _damageMultiplierTimer;

    public Action<float> OnSetDamageMutiplier;
    public Action<float, float> OnChangeDamageTimer;
    public Action<Weapon> OnSetCurrentWeapon;

    public float DamageMultiplier => _damageMultiplier;

    public void SetDamageMultiplier(float multiplier, float duration)
    {
        _damageMultiplier = multiplier;
        _damageMultiplierDuration = duration;
        _damageMultiplierTimer = 0;

        OnSetDamageMutiplier?.Invoke(_damageMultiplier);
        OnChangeDamageTimer?.Invoke(_damageMultiplierTimer, _damageMultiplierDuration);
    }

    public void SetWeapon(WeaponIdentity id)
    {
        _weaponId = id;
        SetCurrentWeapon(_weaponId);
    }

    protected abstract void Shooting();
    protected abstract void Reloading();

    protected override void OnInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _rig = GetComponentInChildren<Rig>();
        _weapons = GetComponentsInChildren<Weapon>(true);

        InitWeapons(_weapons);
        SetDefaultDamageMultiplier();
    }

    protected void Shoot()
    {
        _currentWeapon.Shoot(_damageMultiplier);
    }

    protected bool CheckHasBulletsInRow()
    {
        return _currentWeapon.CheckHasBulletsInRow();
    }

    protected void Reload()
    {
        _currentWeapon.Reload();
        RefreshReloadingAnimation();
    }

    protected void DamageBonusing()
    {
        if(_damageMultiplierDuration <= 0)
        {
            return;
        }

        _damageMultiplierTimer += Time.deltaTime;
        OnChangeDamageTimer?.Invoke(_damageMultiplierTimer, _damageMultiplierDuration);

        if (_damageMultiplierTimer >= _damageMultiplierDuration)
        {
            SetDefaultDamageMultiplier();
        }
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        Shooting();
        Reloading();
        DamageBonusing();
    }

    private void InitWeapons(Weapon[] weapons)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Init();
        }
    }

    private void SetCurrentWeapon(WeaponIdentity identity)
    {
        UnsubscriveFromEndReloading();
        for (int i = 0; i < _weapons.Length; i++)
        {
            Weapon weapon = _weapons[i];
            bool isTargetId = weapon.Id == identity;
            if (isTargetId)
            {
                _currentWeapon = weapon;
                OnSetCurrentWeapon?.Invoke(weapon);
                SubscribeToEndReloading();
            }
            weapon.SetActive(isTargetId);
        }

        int id = WeaponIdentifier.GetAnimationIdByWeaponIdentify(identity);
        _animator.SetInteger(WeaponIdKey, id);
        RefreshReloadingAnimation();
    }

    private void SetDefaultDamageMultiplier()
    {
        SetDamageMultiplier(DefaultDamageMutiplier, 0);
    }


    private void RefreshReloadingAnimation()
    {
        _rig.weight = _currentWeapon.IsReloading ? 0 : 1;
        _animator.SetBool(IsReloadingKey, _currentWeapon.IsReloading);
    }

    private void SubscribeToEndReloading()
    {
        if (!_currentWeapon)
        {
            return;
        }
        _currentWeapon.OnEndReloading += RefreshReloadingAnimation;
    }

    private void UnsubscriveFromEndReloading()
    {
        if (!_currentWeapon)
        {
            return;
        }
        _currentWeapon.OnEndReloading += RefreshReloadingAnimation;
    }

    private void OnDestroy()
    {
        UnsubscriveFromEndReloading();
    }
}
