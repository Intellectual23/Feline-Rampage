using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
  public class HealingEffect: Effect
  {
    
    public HealingEffect(string name, int value, int duration) : base(name, value, duration)
    {
    }

    public override void Action(Unit unit, bool isMainChar)
    {
      
      if (isMainChar)
      {
        Interface.InterfaceLog.Instance.AddMessage($"healing: +{_value} hp");
        Game.Instance.CurrentHealth += _value;
        Interface.InterfaceLog.Instance.AddMessage($"hp: {Game.Instance.CurrentHealth}");
        --_duration;
      }
      else
      {
        Interface.InterfaceLog.Instance.AddMessage($"healing: +{_value} hp");
        unit.UnitSettings.Hp += _value;
        Interface.InterfaceLog.Instance.AddMessage($"hp: {unit.UnitSettings.Hp}");
        --_duration;
      }
    }
  }
}