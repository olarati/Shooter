using System;
using UnityEngine;

public abstract class CharacterWeaponSelector : CharacterPart
{

    [SerializeField] private WeaponIdentity _weaponId;

    public Action<WeaponIdentity> OnWeaponSelected;

    public void RefreshSelectedWeapon()
    {
        OnWeaponSelected?.Invoke(_weaponId);
    }

    protected override void OnInit() 
    {
        RefreshSelectedWeapon();
    }


}
