namespace Unit
{
  public class BleedingEffect: Effect
  {
    public BleedingEffect(string name, int value, int duration) : base(name, value, duration)
    {
    }
    
    public override void Action(Unit unit, bool isMainChar)
    {
      if (!isMainChar)
      {
        Interface.InterfaceLog.Instance.AddMessage($"bleeding: -{_value} hp");
        unit.CurrentHealth -= _value;
        Interface.InterfaceLog.Instance.AddMessage($"hp: {unit.UnitSettings.Hp}");
        --_duration;
      }
      else
      {
        Interface.InterfaceLog.Instance.AddMessage($"bleeding: -{_value} hp");
        Game.Instance.CurrentHealth -= _value;
        Interface.InterfaceLog.Instance.AddMessage($"hp: {Game.Instance.CurrentHealth}");
        --_duration;
      }
      // не заюудь про ситуации когда меньше нуля hp and duration
    }
  }
}