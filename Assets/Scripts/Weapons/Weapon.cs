using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    public abstract WeaponIdentity Id { get; }

    public int Damage
    {
        get
        {
            return _damage;
        }
    } 

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
