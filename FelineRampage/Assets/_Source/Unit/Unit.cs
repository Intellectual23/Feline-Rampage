using System;
using System.ComponentModel.Design.Serialization;

namespace Unit
{
  public class Unit
  {
    private UnitSettings _unitSettings;

    public Unit(UnitSettings unitSettings)
    {
      _unitSettings = unitSettings;
    }

    public void FightMode()
    {
      UnitSettings firstAttacker;
      UnitSettings secondAttacker;
      
      Random rnd = new Random();
      int whoIsFirst = rnd.Next(0, 1);
      if (whoIsFirst == 0)
      {
        firstAttacker = Game.Instance.Settings;
        secondAttacker = _unitSettings;
      }
      else
      {
        firstAttacker = _unitSettings;
        secondAttacker = Game.Instance.Settings;
      }
      
      while (true)
      {
        int firstDamage = CalculateDamage(firstAttacker, rnd);
        // dodged? or receive damage and is dead?
        if (!ReceiveDamage(secondAttacker, rnd, firstDamage))
        {
          // if dead
          break;
        }
        int secondDamage = CalculateDamage(secondAttacker, rnd);
        // dodged? or receive? is dead?
        if (!ReceiveDamage(firstAttacker, rnd, secondDamage))
        {
          break;
        }
      }
    }

    private bool ReceiveDamage(UnitSettings unit, Random rnd, int damage)
    {
      int dodgeProbability = (unit.Agility / 12) * 100;
      int cubeParameter = rnd.Next(1, 100);
      
      // successfully dodged
      if (cubeParameter >= 1 && cubeParameter < dodgeProbability)
      {
        return true;
      }

      unit.Hp -= damage;
      if (unit.Hp <= 0)
      {
        return false;
      }
      return true;
    }
    
    private int CalculateDamage(UnitSettings unit, Random rnd)
    {
      int defaultDamage = unit.Strength * 5;
      int cubeParam = rnd.Next(1, 100);
      int defaultAttackPercentage = 50;
      int criticalAttackPercentage = 10 * unit.Luck;
      int missedAttackPercentage = 100 - defaultAttackPercentage - criticalAttackPercentage;
      // промах
      if (cubeParam >= 1 && cubeParam < missedAttackPercentage)
      {
        return 0;
      }
      // дефолт атака
      if (cubeParam >= missedAttackPercentage && cubeParam < missedAttackPercentage + defaultAttackPercentage)
      {
        return defaultDamage;
      }

      return 2 * defaultDamage;
    }
  }
}