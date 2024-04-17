using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
  public class Settings : MonoBehaviour
  {
    public Slider _slider;
    public TextMeshProUGUI _text;

    public void Start()
    {
      _slider.value = 1;
      //MusicSounds.Instance._musicManager.clip = MusicSounds.Instance._sounds[0];
      //MusicSounds.Instance._musicManager.Play();
      //Debug.Log("music play settings");
    }

    public void Update()
    {
      _text.text = $"{Mathf.RoundToInt(_slider.value*100)}%";
      // setting game volume the value of slider.
    }

    public void Close()
    {
      gameObject.SetActive(false);
    }
  }
}