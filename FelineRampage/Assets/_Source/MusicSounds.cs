
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MusicSounds: MonoBehaviour
{
    public static MusicSounds Instance;
    public AudioClip[] _sounds;
    public AudioSource _musicManager;
    [FormerlySerializedAs("volumeSlider")] public Slider _volumeSlider;
    public int _sceneID;
    
    
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

    public void Start()
    {
        if (_sceneID == 0)
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }
        _musicManager.volume = PlayerPrefs.GetFloat("MusicVolume");
        _volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void Update()
    {
        _musicManager.volume = _volumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", _musicManager.volume);
    }

    public void PlayMusic()
    {
        _musicManager.PlayOneShot(_sounds[0]);
    }
}