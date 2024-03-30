using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
  public Image _bar;
  private void Start()
  {
    
  }

  private void Update()
  {
    _bar.fillAmount = (float)Game.Instance.CurrentHealth / Game.Instance.Settings.Hp;
  }
}