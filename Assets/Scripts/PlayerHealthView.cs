using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Transform _percentsImageTransform;

    private CharacterHealth _playerHealth;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _playerHealth = FindAnyObjectByType<PlayerHealth>();
        _playerHealth.OnAddHealthPoints += Refresh;
    }

    private void Refresh()
    {
        float percents = _playerHealth.GetHealthPoints() / _playerHealth.GetStartHealthPoints();
        percents = Mathf.Clamp01(percents);
        SetPercents(percents);
    }

    private void SetPercents(float value)
    {
        _percentsImageTransform.localScale = new Vector3(value, 1, 1);
    }

    private void OnDestroy()
    {
        _playerHealth.OnAddHealthPoints -= Refresh;
    }
}
