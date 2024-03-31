using UnityEngine;
using Random = System.Random;

namespace Unit
{
  public class Unit
  {
    private UnitSettings _startSettings;
    private UnitSettings _unitSettings;
    public bool IsFighting { get; set; }

    public Unit(UnitSettings unitSettings)
    {
      _startSettings = unitSettings;
      _unitSettings = new();
      _unitSettings.Hp = _startSettings.Hp;
      _unitSettings.Strength = _startSettings.Strength;
      _unitSettings.Agility = _startSettings.Agility;
      _unitSettings.Luck = _startSettings.Luck;
    }
    
    public UnitSettings UnitSettings
    {
      get => _unitSettings;
      set => _unitSettings = value;
    }

    public int CurrentHealth
    {
      get => _unitSettings.Hp;
      set => _unitSettings.Hp = value;
    }
  }
}