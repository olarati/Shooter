using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerPartBase _playerMovement;
    private PlayerPartBase _playerAiming;

    private PlayerPartBase[] _playerParts;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAiming = GetComponent<PlayerAiming>();

        _playerParts = new PlayerPartBase[]
        {
            _playerMovement,
            _playerAiming
        };

        for (int i = 0; i < _playerParts.Length; i++)
        {
            _playerParts[i].Init();
        }
    }



}
