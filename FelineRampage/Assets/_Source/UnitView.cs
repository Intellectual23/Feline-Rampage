using UnityEngine;
using UnityEngine.PlayerLoop;

public class UnitView : MonoBehaviour
  {
    private Unit _unit;

    public void Init(Unit unit)
    {
      _unit = unit;
    }
  }