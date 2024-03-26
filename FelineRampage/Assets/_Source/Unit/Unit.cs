using UnityEngine;
using Random = System.Random;

namespace Unit
{
  public class Unit
  {
    private UnitSettings _unitSettings;
    public bool IsFighting { get; set; }

    public Unit(UnitSettings unitSettings)
    {
      _unitSettings = unitSettings;
    }

    public UnitSettings UnitSettings
    {
      get => _unitSettings;
      set => _unitSettings = value;
    }
  }
}