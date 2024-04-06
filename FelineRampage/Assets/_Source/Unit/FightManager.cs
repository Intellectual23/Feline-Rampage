using System;
using System.Collections;
using System.Data;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;
using System.Linq;

// TODO: ПОРЕНЕЙМИТЬ, счёты уменьшить и кодстайл, OPTIMIZATION пожалуйта ради бога 

namespace Unit
{
  public class FightManager : MonoBehaviour
  {
    public static FightManager Instance;
    public Unit _enemy; // враг
    public string Part { get; set; }
    public bool playerTurn = true;
    Random rnd = new Random();
    private List<Effect> unitEffects = new();
    private List<Effect> mainCharEffects = new();
    public List<Effect> allEffects = new();
    private int typeOfAttack = 0; // 0 missed, 1 - default, 2 - critical
    
    // игнорится вдруг от эффектов станет отриц здоровье....
    
    private void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
      }
      else
      {
        Destroy(gameObject);
      }
    }

    public void Start()
    {
      // + от крысы с простой атаки c шансом 30 на 70
      allEffects.Add(new PoisoningEffect("poisoned", 2, 6)); //0
      // + с простой атаки со склетеа и гоблина 30 на 70
      allEffects.Add(new BleedingEffect("bleeding", 4, 3)); // 1
      // + по голове с 50 на 50 вероятностью
      allEffects.Add(new StunEffect("stun", 0, 3)); // 2
      // + когда уклонился с 50 на 50 вероятностью
      allEffects.Add(new HealingEffect("heal", 2, 4)); //3
    }

    public void Init(Unit unit)
    {
      _enemy = unit;
    }

    public void StartFight()
    {
      Interface.InterfaceLog.Instance.AddMessage("FIGHT MODE");
      StartCoroutine(FightMode());
    }

    private IEnumerator FightMode()
    {
      // 0 - main character, 1 - mob
      int whoIsFirst = rnd.Next(0, 2);
      if (whoIsFirst == 1)
      {
        Interface.InterfaceLog.Instance.AddMessage("enemy is the first");
        playerTurn = false;
      }
      else
      {
        Interface.InterfaceLog.Instance.AddMessage("you are the first");
      }

      while (Game.Instance.CurrentHealth > 0 && _enemy.UnitSettings.Hp > 0)
      {
        if (playerTurn && Game.Instance.CurrentHealth > 0 && _enemy.UnitSettings.Hp > 0)
        {
          yield return StartCoroutine(PlayerTurn());
        }
        else if (!playerTurn && Game.Instance.CurrentHealth > 0 && _enemy.UnitSettings.Hp > 0)
        {
          Debug.Log(_enemy.UnitSettings.Hp);
          yield return StartCoroutine(EnemyTurn());
        }
        else
        {
          _enemy.IsFighting = false;
          Interface.InterfaceLog.Instance.AddMessage("the end of the battle.");
          yield break;
        }

        playerTurn = !playerTurn;
      }

      yield return null;
    }

    private IEnumerator PlayerTurn()
    {
      Interface.InterfaceLog.Instance.AddMessage("your turn.");
      if (mainCharEffects.Count != 0)
      {
        foreach (Effect effect in mainCharEffects)
        {
          if (effect._duration <= 0)
          {
            mainCharEffects.Remove(effect);
            continue;
          }
          effect.Action(_enemy, true);
        }
      }

      Interface.InterfaceLog.Instance.AddMessage("waiting for a click");
      bool isRightClickReceived = false;

      while (!isRightClickReceived)
      {
        // Ждем клика по юниту
        yield return new WaitUntil(() => Input.GetMouseButtonDown(1));
        // ColliderHandler.PartDetection();
        // Наносим урон юниту
        switch (Part)
        {
          case "Head":
            MainCharAttackToHead(Game.Instance.Settings);
            isRightClickReceived = true;
            break;
          case "Legs":
            MainCharAttackToLegs(Game.Instance.Settings);
            isRightClickReceived = true;
            break;
          case "Body":
            MainCharAttackToBody();
            isRightClickReceived = true;
            break;
          default:
            Interface.InterfaceLog.Instance.AddMessage("click retry");
            break;
        }
      }
      isRightClickReceived = false;
    }

    private IEnumerator EnemyTurn()
    {
      Interface.InterfaceLog.Instance.AddMessage("enemy's turn");
      if (unitEffects.Count != 0)
      {
        foreach (Effect effect in unitEffects)
        {
          if (effect._duration <= 0)
          {
            unitEffects.Remove(effect);
            continue;
          }
          effect.Action(_enemy, false);
        }
      }
      Interface.InterfaceLog.Instance.AddMessage("enemy's thinking..");
      yield return new WaitForSeconds(4f);
      if (_enemy.UnitSettings.Hp > 0)
      {
        // Юнит наносит урон игроку
        MobTurn();
      }
    }

    private void MainCharAttackToBody()
    {
      int mainCharacterAttackValue = MainCharacterAttack(Game.Instance.Settings.Luck, false);
      Interface.InterfaceLog.Instance.AddMessage($"main character's attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = _enemy.UnitSettings.Agility;
      if (ReceiveDamage(mobAgilityToUse, _enemy.UnitSettings, false, mainCharacterAttackValue, false)) return;
      Interface.InterfaceLog.Instance.AddMessage("mob was defeated");
    }

    private void MainCharAttackToHead(UnitSettings unit)
    {
      // у гг уменьшается удача а значит уменьшается вероятность крита 
      // но если крит случается то он икс два 
      int newLuck = unit.Luck - (unit.Luck / 2);
      int mainCharacterAttackValue = MainCharacterAttack(newLuck, true);
      Interface.InterfaceLog.Instance.AddMessage($"main character's attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = _enemy.UnitSettings.Agility;
      if (ReceiveDamage(mobAgilityToUse, _enemy.UnitSettings, false, mainCharacterAttackValue, false)) return;
      Interface.InterfaceLog.Instance.AddMessage("mob was defeated");
    }

    public void MainCharAttackToLegs(UnitSettings unit)
    {
      // у врага больше ловкости чтоб отбиться 
      // но если игрок попадает то урон + ловкость уменьшается
      int mainCharacterAttackValue = MainCharacterAttack(unit.Luck, false);
      Interface.InterfaceLog.Instance.AddMessage($"main character's attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = _enemy.UnitSettings.Agility;
      if (mobAgilityToUse >= 8)
      {
        mobAgilityToUse = 10;
      }
      else
      {
        mobAgilityToUse += 2;
      }

      if (ReceiveDamage(mobAgilityToUse, _enemy.UnitSettings, false, mainCharacterAttackValue, true)) return;
      Interface.InterfaceLog.Instance.AddMessage("mob was defeated");
    }

    private int MainCharacterAttack(int luck, bool toHead)
    {
      int strength = Game.Instance.Settings.Strength;
      int defaultDamage = strength * 5;
      int cubeParam = rnd.Next(1, 101);
      int defaultAttackPercentage = 50;
      int criticalAttackPercentage = 10 * luck;
      int missedAttackPercentage = 100 - defaultAttackPercentage - criticalAttackPercentage;
      // промах
      if (cubeParam >= 1 && cubeParam < missedAttackPercentage)
      {
        typeOfAttack = 0;
        return 0;
      }

      // дефолт атака
      if (cubeParam >= missedAttackPercentage && cubeParam < missedAttackPercentage + defaultAttackPercentage)
      {
        typeOfAttack = 1;
        return defaultDamage;
      }

      typeOfAttack = 3;
      int effectProbability = rnd.Next(1, 3);
      if (effectProbability == 1)
      {
        // мобу оглушение
        unitEffects.Add(allEffects[2]);
      }
      
      if (toHead)
      {
        return 3 * defaultDamage;
      }

      return 2 * defaultDamage;
    }

    private void MobTurn()
    {
      int mobAttackValue = MobAttack(_enemy.UnitSettings);
      Interface.InterfaceLog.Instance.AddMessage($"mob's attack: {mobAttackValue}");
      if (ReceiveDamage(Game.Instance.Settings.Agility, Game.Instance.Settings, true, mobAttackValue, false)) return;
      Interface.InterfaceLog.Instance.AddMessage("main char was defeated");
    }

    private int MobAttack(UnitSettings unit)
    {
      int defaultDamage = unit.Strength * 5;
      int cubeParam = rnd.Next(1, 101);
      int defaultAttackPercentage = 50;
      int criticalAttackPercentage = 10 * unit.Luck;
      int missedAttackPercentage = 100 - defaultAttackPercentage - criticalAttackPercentage;
      // промах
      if (cubeParam >= 1 && cubeParam < missedAttackPercentage)
      {
        typeOfAttack = 0;
        return 0;
      }

      // дефолт атака
      if (cubeParam >= missedAttackPercentage && cubeParam < missedAttackPercentage + defaultAttackPercentage)
      {
        typeOfAttack = 1;
        return defaultDamage;
      }

      typeOfAttack = 2;

      return 2 * defaultDamage;
    }

    private bool ReceiveDamage(int agilityToUse, UnitSettings unit, bool mainCharReceive, int damage, bool toLegs)
    {
      int dodgeProbability = (agilityToUse * 3);
      int cubeParameter = rnd.Next(1, 101);

      // successfully dodged
      if (cubeParameter >= 1 && cubeParameter < dodgeProbability)
      {
        Interface.InterfaceLog.Instance.AddMessage("opponent dodged");
        if (mainCharReceive)
        {
          // healing effect
          mainCharEffects.Add(allEffects[3]);
        }
        else
        {
          unitEffects.Add(allEffects[3]);
        }
        return true;
      }

      ReceiveEffects(mainCharReceive);
      
      if (mainCharReceive)
      {
        Interface.InterfaceLog.Instance.AddMessage($"hp: {Game.Instance.CurrentHealth} -> {Game.Instance.CurrentHealth -= damage}");

        return Game.Instance.CurrentHealth > 0;
      }
      else
      {
        Interface.InterfaceLog.Instance.AddMessage($"hp: {_enemy.UnitSettings.Hp} -> {_enemy.UnitSettings.Hp -= damage}");
        if (toLegs)
        {
          Interface.InterfaceLog.Instance.AddMessage($"new agility: {unit.Agility -= (unit.Agility / 2)}");
        }

        return _enemy.UnitSettings.Hp > 0;
      }
    }

    private void ReceiveEffects(bool mainCharReceive)
    {
      if (mainCharReceive)
      {
        if (typeOfAttack == 1 && _enemy.UnitSettings.name.Contains("Rat"))
        {
          int possibility = rnd.Next(1, 8);
          if (possibility <= 3)
          {
            mainCharEffects.Add(allEffects[0]);
            mainCharEffects = mainCharEffects.Distinct().ToList();
            Interface.InterfaceLog.Instance.AddMessage($"+ poisoned");
          }
        } else if (typeOfAttack == 1 && (_enemy.UnitSettings.name.Contains("Goblin") || _enemy.UnitSettings.name.Contains("Skeleto")))
        {
          int possibility = rnd.Next(1, 8);
          if (possibility <= 3)
          {
            mainCharEffects.Add(allEffects[1]);
            mainCharEffects = mainCharEffects.Distinct().ToList();
            Interface.InterfaceLog.Instance.AddMessage($"+ bleeding");
          }
        } else if (typeOfAttack == 2)
        {
          int possibility = rnd.Next(1, 3);
          if (possibility == 1)
          {
            mainCharEffects.Add(allEffects[2]);
            mainCharEffects = mainCharEffects.Distinct().ToList();
            Interface.InterfaceLog.Instance.AddMessage($"+ stun");
          }
        }
      }
      else
      {
        if (typeOfAttack == 1)
        {
          int possibility = rnd.Next(1, 8);
          if (possibility <= 3)
          {
            unitEffects.Add(allEffects[1]);
            unitEffects = unitEffects.Distinct().ToList();
            Interface.InterfaceLog.Instance.AddMessage($"+ bleeding");
          }
        } else if (typeOfAttack == 2)
        {
          int possibility = rnd.Next(1, 3);
          if (possibility == 1)
          {
            unitEffects.Add(allEffects[2]);
            unitEffects = unitEffects.Distinct().ToList();
            Interface.InterfaceLog.Instance.AddMessage($"+ stun");
          }
        }
      }
    }
  }
}