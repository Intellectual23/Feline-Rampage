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
        unit.UnitSettings.Hp -= _value;
      }
      else
      {
        Game.Instance.CurrentHealth -= _value;
      }
      
    }
  }
}