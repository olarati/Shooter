using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthViewsController : MonoBehaviour
{
    [SerializeField] private CharacterHealthView _enemyHealthViewPrefab;
    [SerializeField] private Transform _enemyHealthViewsContainer;
    [SerializeField] private Vector3 _deltaHealthViewPosition = new Vector3(0, 2.2f, 0);

    private Dictionary<CharacterHealth, CharacterHealthView> _enemyHealthViewPairs = new Dictionary<CharacterHealth, CharacterHealthView>();
    private Camera _mainCamera;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        CharacterHealth[] enemyHealths = FindObjectsOfType<EnemyHealth>();
        for (int i = 0; i < enemyHealths.Length; i++)
        {
            CharacterHealthView characterHeathView = Instantiate(_enemyHealthViewPrefab, _enemyHealthViewsContainer);
            characterHeathView.Init(enemyHealths[i]);
            _enemyHealthViewPairs.Add(enemyHealths[i], characterHeathView);
        }
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        RefreshViewsPositions();
    }

    private void RefreshViewsPositions()
    {
        foreach (var pair in _enemyHealthViewPairs)
        {
            pair.Value.transform.position = _mainCamera.WorldToScreenPoint(pair.Key.transform.position + _deltaHealthViewPosition);
        }
    }

}
