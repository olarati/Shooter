using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private CharacterMovement _movement;
    private CharacterAiming _aiming;
    private CharacterShooting _shooting;
    private CharacterHealth _health;

    private CharacterPart[] _parts;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _movement = GetComponent<CharacterMovement>();
        _aiming = GetComponent<CharacterAiming>();
        _shooting = GetComponent<CharacterShooting>();
        _health = GetComponent<CharacterHealth>();

        _parts = new CharacterPart[]
        {
            _movement,
            _aiming,
            _shooting,
            _health
        };

        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i])
            {
                _parts[i].Init();
            }
        }

        _health.OnDie += Stop;
    }

    private void Stop()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i])
            {
                _parts[i].Stop();
            }
        }
    }
}
