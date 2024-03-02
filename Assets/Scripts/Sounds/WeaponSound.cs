using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;

    private AudioSource _audioSource;

    public void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType type)
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            Sound sound = _sounds[i];
            if (sound.Type == type)
            {
                _audioSource.PlayOneShot(sound.Clip, sound.Volume);
            }
        }
    }

}

[System.Serializable]
public class Sound
{
    [SerializeField] private SoundType _type;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _volume;

    public SoundType Type => _type;
    public AudioClip Clip => _clip;
    public float Volume => _volume;
}

public enum SoundType
{
    Shoot,
    Reload,
    Switch
}
