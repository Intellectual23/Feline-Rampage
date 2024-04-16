
using UnityEngine;
using UnityEngine.Serialization;

public class MusicSounds: MonoBehaviour
{
    public static MusicSounds Instance;
    public AudioClip[] _sounds;
    public AudioSource _musicManager;

    public void PlayMusic()
    {
        _musicManager.clip = _sounds[0];
        _musicManager.Play();
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    
    }
}