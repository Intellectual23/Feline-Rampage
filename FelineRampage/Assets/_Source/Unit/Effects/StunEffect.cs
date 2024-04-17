namespace Unit
{
  public class StunEffect: Effect
  {
    private int Luck;
    private int _maxDuration;
    public StunEffect(string name, int value, int duration) : base(name, value, duration)
    {
      _maxDuration = duration;
    }

    public override void Action(Unit unit, bool isMainChar)
    {
      if (!isMainChar)
      {
        if (_maxDuration == _duration)
        {
          Luck = unit.UnitSettings.Luck;
        }

        Interface.InterfaceLog.Instance.AddMessage($"stun: luck = 0");
        unit.UnitSettings.Luck = 0;
        _duration--;
        if (_duration == 0)
        {
          unit.UnitSettings.Luck = Luck;
          return;
        }
      }
      else
      {
        if (_maxDuration == _duration)
        {
          Luck = Game.Instance.Settings.Luck;
        }

        Interface.InterfaceLog.Instance.AddMessage($"stun: luck = 0");
        Game.Instance.Settings.Luck = 0;
        _duration--;
        if (_duration == 0)
        {
          Game.Instance.Settings.Luck = Luck;
          return;
        }
      }
    }
  }
}