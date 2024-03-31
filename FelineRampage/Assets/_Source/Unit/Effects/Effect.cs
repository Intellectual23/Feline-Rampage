namespace Unit
{
  public abstract class Effect
  {
    public string _name;
    public int _value;
    public int _duration;

    public Effect(string name, int value, int duration)
    {
      _duration = duration;
      _name = name;
      _value = value;
    }
    
    public abstract void Action(Unit unit, bool isMainChar);
  }
}