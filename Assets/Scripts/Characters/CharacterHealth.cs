using System;
using UnityEngine;

public abstract class CharacterHealth : CharacterPart
{
    private const string DeathKey = "Death";

    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private int _startHealthPoints = 100;

    private Animator _animator;
    private AudioSource _audioSource;

    private int _healthPoints;
    private bool _isDead;

    public Action OnDie;
    public Action<CharacterHealth> OnDieWithObject;
    public Action OnAddHealthPoints;

    public void AddHealthPoints(int value)
    {
        if (_isDead)
        {
            return;
        }

        _healthPoints += value;
        Mathf.Clamp(_healthPoints, 0, _startHealthPoints);
        OnAddHealthPoints?.Invoke();

        if (_healthPoints <= 0)
        {
            Die();
        }
    }

    public int GetStartHealthPoints()
    {
        return _startHealthPoints;
    }
    public int GetHealthPoints()
    {
        return _healthPoints;
    }

    protected override void OnInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _healthPoints = _startHealthPoints;
        _isDead = false;
    }

    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger(DeathKey);
        _audioSource.PlayOneShot(_deathClip);
        OnDie?.Invoke();
        OnDieWithObject?.Invoke(this);
    }


}
