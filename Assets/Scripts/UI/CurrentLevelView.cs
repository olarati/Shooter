using TMPro;
using UnityEngine;

public class CurrentLevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        SetLevelText(GameStateChanger.Level);
    }

    private void SetLevelText(int level)
    {
        _levelText.text = $"Уровень {level}";
    }
}
