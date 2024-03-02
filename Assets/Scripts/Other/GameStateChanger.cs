using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateChanger : MonoBehaviour
{
    private const string LevelKey = "Level";
    private static int _level;

    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;

    private CharacterHealth _playerHealth;
    private EnemySpawner _enemySpawner;
    private Screen[] _screens;
    private AudioSource _audioSource;

    public static int Level
    {
        get
        {
            if(_level <= 0)
            {
                _level = PlayerPrefs.GetInt(LevelKey, 1);
            }
            return _level;
        }
        set
        {
            _level = value;
            PlayerPrefs.SetInt(LevelKey, _level);
        }
    }

    public void NextLevel()
    {
        Level++;
        LoadScene();
    }

    public void RestartLevel()
    {
        LoadScene();
    }

    public void DropLevel()
    {
        Level = 1;
        LoadScene();
    }

    private void LoadScene()
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
        _audioSource = GetComponentInChildren<AudioSource>();

        _playerHealth.OnDie += LoseGame;
        _enemySpawner.OnAllEnemiesDie += WinGame;

        ShowScreen<GameScreen>();
    }

    private void LoseGame()
    {
        ShowScreen<GameLoseScreen>();
        _audioSource.PlayOneShot(_loseClip);
    }

    private void WinGame()
    {
        ShowScreen<GameWinScreen>();
        _audioSource.PlayOneShot(_winClip);
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
