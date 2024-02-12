using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCurrentWeaponView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;

    [SerializeField] private Sprite _rifleSprite;
    [SerializeField] private Sprite _pistolSprite;
    [SerializeField] private Sprite _shotgunSprite;

    private Dictionary<WeaponIdentity, Sprite> _weaponToSpritePairs;
    private CharacterWeaponSelector _playerWeaponSelector;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _weaponToSpritePairs = new Dictionary<WeaponIdentity, Sprite>()
        {
            { WeaponIdentity.Rifle, _rifleSprite },
            { WeaponIdentity.Pistol, _pistolSprite },
            { WeaponIdentity.Shotgun, _shotgunSprite }
        };

        _playerWeaponSelector = FindObjectOfType<PlayerWeaponSelector>();
        _playerWeaponSelector.OnWeaponSelected += SetIconByType;
    }

    private void SetIconByType(WeaponIdentity weaponId)
    {
        _iconImage.sprite = _weaponToSpritePairs[weaponId];
    }

    private void OnDestroy()
    {
        if (_playerWeaponSelector)
        {
            _playerWeaponSelector.OnWeaponSelected -= SetIconByType;
        }
    }
}
