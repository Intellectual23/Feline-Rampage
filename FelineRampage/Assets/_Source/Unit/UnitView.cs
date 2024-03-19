using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Unit
{
  public class UnitView : MonoBehaviour
  {
    private Unit _unit;

    public void Init(Unit unit)
    {
      _unit = unit;
    }
  }
}