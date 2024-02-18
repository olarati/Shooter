using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateChanger : MonoBehaviour
{
    public static int Level = 1;

    private CharacterHealth _playerHealth;
    private EnemySpawner _enemySpawner;
    private Screen[] _screens;

    public void NextLevel()
    {
        Level++;
        LoadScene();
    }

    public void RestartLevel()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        ShowScreen<LoadingScreen>();
        SceneManager.LoadSceneAsync(0);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _screens = FindObjectsOfType<Screen>(true);

        _playerHealth.OnDie += LoseGame;
        _enemySpawner.OnAllEnemiesDie += WinGame;

        ShowScreen<GameScreen>();
    }

    private void LoseGame()
    {
        ShowScreen<GameLoseScreen>();
    }

    private void WinGame()
    {
        ShowScreen<GameWinScreen>();
    }

    private void ShowScreen<T>() where T : Screen
    {
        for (int i = 0; i < _screens.Length; i++)
        {
            _screens[i].SetActive(_screens[i] is T);
        }
    }

    private void OnDestroy()
    {
        if (_playerHealth)
        {
            _playerHealth.OnDie -= LoseGame;
        }
        if (_enemySpawner)
        {
            _enemySpawner.OnAllEnemiesDie -= WinGame;
        }
    }
}
