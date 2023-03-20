using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    [SerializeField] private AudioClip _tileClickClip;
    [SerializeField] private AudioClip _matchedSound;
    [SerializeField] private float _minPitch = 0.5f;
    [SerializeField] private float _maxPitch = 1.5f;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayTileClickSound(int selectedTileCount)
    {
        float pitch = _minPitch + selectedTileCount * 0.2f;
        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(_tileClickClip);
    }
    public void PlayMatchedSound()
    {
        _audioSource.PlayOneShot(_matchedSound);
    }
}
