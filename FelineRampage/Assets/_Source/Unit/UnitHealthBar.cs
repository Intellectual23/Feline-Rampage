using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
  public class UnitHealthBar: MonoBehaviour
  {
    public Slider healthSlider;
    public float maxHealth = 20f;
    
    private void Start()
    {
      healthSlider.value = maxHealth;
    }
  }
}