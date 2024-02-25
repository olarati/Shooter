using UnityEngine;
using Random = System.Random;

public class LocationSelector : MonoBehaviour
{
    // ����� � ����������, ����� � ������� ���� ������������ ������������������
    // �.�. ���� ������ �� ����, �� ������������������ ����� ���� ���������
    // � ����� �� ��������� � ������ ������� �������������, ��������� ����� ���������
    [SerializeField] private Location[] _locations;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SelectRandomLocation(GameStateChanger.Level);
    }

    private void SelectRandomLocation(int seed)
    {
        Random random = new Random(seed);
        int selectedId = random.Next(_locations.Length);
        
        for (int i = 0; i < _locations.Length; i++)
        {
            _locations[i].SetActive(i == selectedId);
        }
    }
}
