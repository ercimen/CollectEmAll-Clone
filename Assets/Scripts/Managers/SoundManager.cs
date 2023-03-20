using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    [SerializeField] private AudioClip _tileClickClip;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayTileClickSound(int selectedTileCount)
    {
        _audioSource.PlayOneShot(_tileClickClip);
    }
}
