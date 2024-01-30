using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    public int Damage
    {
        get
        {
            return _damage;
        }
    } 
}
