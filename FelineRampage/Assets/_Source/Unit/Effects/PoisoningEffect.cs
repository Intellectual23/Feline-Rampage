namespace Unit
{
  public class PoisoningEffect: Effect
  {
    public PoisoningEffect(string name, int value, int duration) : base(name, value, duration)
    {
    }
    
    public override void Action(Unit unit, bool isMainChar)
    {
      if (!isMainChar)
      {
        Interface.InterfaceLog.Instance.AddMessage($"poisoned: -{_value} hp");
        unit.UnitSettings.Hp -= _value;
        Interface.InterfaceLog.Instance.AddMessage($"hp: {unit.UnitSettings.Hp}");
      }
      else
      {
        Interface.InterfaceLog.Instance.AddMessage($"poisoned: -{_value} hp");
        Game.Instance.CurrentHealth -= _value;
        Interface.InterfaceLog.Instance.AddMessage($"hp: {Game.Instance.CurrentHealth}");
      }
      
    }
  }
}