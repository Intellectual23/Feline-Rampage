namespace Unit
{
  public class BleedingEffect: Effect
  {
    public BleedingEffect(string name, int value, int duration) : base(name, value, duration)
    {
    }
    
    public override void Action(Unit unit, bool isMainChar)
    {
      if (isMainChar == false)
      {
        unit.CurrentHealth -= _value;
        --_duration;
      }
      else
      {
        Game.Instance.CurrentHealth -= _value;
        --_duration;
      }
      // не заюудь про ситуации когда меньше нуля
    }
  }
}