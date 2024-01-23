using UnityEngine;

public interface IPhysicHitable
{
    void Hit(Vector3 force, Vector3 position);
}