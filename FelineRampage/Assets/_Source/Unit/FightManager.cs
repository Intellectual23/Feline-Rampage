using System;
using System.Collections;
using System.Data;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;

// TODO: ПОРЕНЕЙМИТЬ, счёты уменьшить и кодстайл

namespace Unit
{
  public class FightManager : MonoBehaviour
  {
    public static FightManager Instance;
    public Unit _enemy; // враг
    public string Part { get; set; }
    public bool playerTurn = true;
    Random rnd = new Random();
    private List<Effect> unitEffects;
    private List<Effect> mainCharEffects;
    private List<Effect> allEffects = new();

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

      DontDestroyOnLoad(this);
    }

    public void Start()
    {
      allEffects.Add(new PoisoningEffect("poisoned", 2, 6));
      allEffects.Add(new BleedingEffect("bleeding", 4, 3));
      allEffects.Add(new StunEffect("stun", 0, 3));
    }

    public void Init(Unit unit)
    {
      _enemy = unit;
    }

    public void StartFight()
    {
      StartCoroutine(FightMode());
      Debug.Log("StartFight");
    }

    public IEnumerator FightMode()
    {
      Debug.Log("fight mode coroutine");
      // 0 - main character, 1 - mob
      int whoIsFirst = rnd.Next(0, 2);
      if (whoIsFirst == 1)
      {
        playerTurn = false;
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
          Debug.Log("Battle ended.");
          yield break;
        }

        playerTurn = !playerTurn;
      }

      yield return null;
    }

    private IEnumerator PlayerTurn()
    {
      Debug.Log("Player's turn. Click on the enemy to attack.");

      bool isRightClickReceived = false;

      while (!isRightClickReceived)
      {
        // Ждем клика по юниту
        yield return new WaitUntil(() => Input.GetMouseButtonDown(1));
        //ColliderHandler.PartDetection();
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
            Debug.Log("click retry");
            break;
        }
      }

      isRightClickReceived = false;
    }

    private IEnumerator EnemyTurn()
    {
      yield return new WaitForSeconds(2);
      if (_enemy.UnitSettings.Hp > 0)
      {
        Debug.Log("Enemy's turn. Attacking player.");
        // Юнит наносит урон игроку
        MobTurn();
      }
    }

    private void MainCharAttackToBody()
    {
      int mainCharacterAttackValue = MainCharacterAttack(Game.Instance.Settings.Luck, false);
      Debug.Log($"main character's turn. his attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = _enemy.UnitSettings.Agility;
      if (ReceiveDamage(mobAgilityToUse, _enemy.UnitSettings, false, mainCharacterAttackValue, false)) return;
      Debug.Log("mob was defeated. death");
    }

    private void MainCharAttackToHead(UnitSettings unit)
    {
      // у гг уменьшается удача а значит уменьшается вероятность крита 
      // но если крит случается то он икс два 
      int newLuck = unit.Luck - (unit.Luck / 2);
      int mainCharacterAttackValue = MainCharacterAttack(newLuck, true);
      Debug.Log($"main character's turn. his attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = _enemy.UnitSettings.Agility;
      if (ReceiveDamage(mobAgilityToUse, _enemy.UnitSettings, false, mainCharacterAttackValue, false)) return;
      Debug.Log("mob was defeated. death");
    }

    public void MainCharAttackToLegs(UnitSettings unit)
    {
      // у врага больше ловкости чтоб отбиться 
      // но если игрок попадает то урон + ловкость уменьшается
      int mainCharacterAttackValue = MainCharacterAttack(unit.Luck, false);
      Debug.Log($"main character's turn. his attack: {mainCharacterAttackValue}");
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
      Debug.Log("mob was defeated. death");
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
        return 0;
      }

      // дефолт атака
      if (cubeParam >= missedAttackPercentage && cubeParam < missedAttackPercentage + defaultAttackPercentage)
      {
        return defaultDamage;
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
      Debug.Log($"mob's turn. his attack: {mobAttackValue}");
      if (ReceiveDamage(Game.Instance.Settings.Agility, Game.Instance.Settings, true, mobAttackValue, false)) return;
      Debug.Log("main char was defeated");
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
        return 0;
      }

      // дефолт атака
      if (cubeParam >= missedAttackPercentage && cubeParam < missedAttackPercentage + defaultAttackPercentage)
      {
        return defaultDamage;
      }

      return 2 * defaultDamage;
    }

    private bool ReceiveDamage(int agilityToUse, UnitSettings unit, bool mainCharReceive, int damage, bool toLegs)
    {
      int dodgeProbability = (agilityToUse * 3);
      int cubeParameter = rnd.Next(1, 101);

      // successfully dodged
      if (cubeParameter >= 1 && cubeParameter < dodgeProbability)
      {
        Debug.Log("opponent dodged");
        return true;
      }

      if (mainCharReceive)
      {
        Debug.Log($"opponent haven't dodged and got attacked. hp before attach: {Game.Instance.CurrentHealth}");
        Game.Instance.CurrentHealth -= damage;
        Debug.Log($"his hp after: {Game.Instance.CurrentHealth}");
        if (toLegs)
        {
          unit.Agility -= (unit.Agility / 2);
        }

        return Game.Instance.CurrentHealth > 0;
      }
      else
      {
        Debug.Log($"opponent haven't dodged and got attacked. hp before attach: {_enemy.UnitSettings.Hp}");
        _enemy.UnitSettings.Hp -= damage;
        Debug.Log($"his hp after: {_enemy.UnitSettings.Hp}");
        if (toLegs)
        {
          unit.Agility -= (unit.Agility / 2);
        }

        return _enemy.UnitSettings.Hp > 0;
      }
    }
  }
}