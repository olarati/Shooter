using System;
using UnityEngine;

public class CharacterHealth : CharacterPart
{
    private const string DeathKey = "Death";

    [SerializeField] private int _startHealthPoints = 100;

    private Animator _animator;

    private int _healthPoints;
    private bool _isDead;

    public Action OnDie;

    public void AddHealthPoints(int value)
    {
        if (_isDead)
        {
            return;
        }

        _healthPoints += value;
        if(_healthPoints <= 0)
        {
            Die();
        }
    }

    protected override void OnInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthPoints = _startHealthPoints;
        _isDead = false;
    }

    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger(DeathKey);
        OnDie?.Invoke();
    }


}
