using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

// TODO: ПОРЕНЕЙМИТЬ, счёты уменьшить и кодстайл

namespace Unit
{
  public class FightManager: MonoBehaviour
  {
    public static FightManager Instance;
    public static Unit Enemy { get; set; } // враг
    public static String Part { get; set; }
    public bool playerTurn = true;
    Random rnd = new Random();

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
    
    public IEnumerator FightMode()
    {
      Debug.Log("fight mode coroutine");
      // 0 - main character, 1 - mob
      int whoIsFirst = rnd.Next(0, 2);
      if (whoIsFirst == 1)
      {
        playerTurn = false;
      }
      while (Game.Instance.CurrentHealth > 0 && Enemy.UnitSettings.Hp > 0)
      {
        if (playerTurn && Game.Instance.CurrentHealth > 0 && Enemy.UnitSettings.Hp > 0)
        {
          yield return StartCoroutine(PlayerTurn());
        }
        else if (!playerTurn && Game.Instance.CurrentHealth > 0 && Enemy.UnitSettings.Hp > 0)
        {
          Debug.Log(Enemy.UnitSettings.Hp);
          yield return StartCoroutine(EnemyTurn());
        }
        else
        {
          Enemy.IsFighting = false;
          Debug.Log("Battle ended.");
          yield break;
        }
        playerTurn = !playerTurn;
      }
    }

    private IEnumerator PlayerTurn()
    {
      Debug.Log("Player's turn. Click on the enemy to attack.");

      bool isRightClickReceived = false;

      while (!isRightClickReceived)
      {
        // Ждем клика по юниту
        yield return new WaitUntil(() => Input.GetMouseButtonDown(1));
      
        // Наносим урон юниту
        switch (Part)
        {
          case "Head":
            MainCharAttackToHead(Game.Instance.Settings, rnd);
            isRightClickReceived = true;
            break;
          case "Legs":
            MainCharAttackToLegs(Game.Instance.Settings, rnd);
            isRightClickReceived = true;
            break;
          case "Body":
            MainCharAttackToBody(Game.Instance.Settings, rnd);
            isRightClickReceived = true;
            break;
          default:
            Debug.Log("click retry");
            break;
        }
      }
      
      yield return null;
    }

    private IEnumerator EnemyTurn()
    {
      yield return new WaitForSeconds(10);
      if (Enemy.UnitSettings.Hp > 0)
      {
        Debug.Log("Enemy's turn. Attacking player.");
        // Юнит наносит урон игроку
        MobTurn(rnd);
      }
    }
    
    private void MainCharAttackToBody(UnitSettings unit, Random rnd)
    {
      int mainCharacterAttackValue = MainCharacterAttack(unit.Strength, unit.Luck, rnd, false);
      Debug.Log($"main character's turn. his attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = Enemy.UnitSettings.Agility;
      if (ReceiveDamage(mobAgilityToUse, Enemy.UnitSettings, Enemy.UnitSettings.Hp, rnd, mainCharacterAttackValue, false)) return;
      Debug.Log("mob was defeated. death");
    }

    private void MainCharAttackToHead(UnitSettings unit, Random rnd)
    {
      // у гг уменьшается удача а значит уменьшается вероятность крита 
      // но если крит случается то он икс два 
      int newLuck = unit.Luck - (unit.Luck / 2);
      int mainCharacterAttackValue = MainCharacterAttack(unit.Strength, newLuck, rnd, true);
      Debug.Log($"main character's turn. his attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = Enemy.UnitSettings.Agility;
      if (ReceiveDamage(mobAgilityToUse, Enemy.UnitSettings, Enemy.UnitSettings.Hp, rnd, mainCharacterAttackValue, false)) return;
      Debug.Log("mob was defeated. death");
    }

    public void MainCharAttackToLegs(UnitSettings unit, Random rnd)
    {
      // у врага больше ловкости чтоб отбиться 
      // но если игрок попадает то урон + ловкость уменьшается
      int newLuck = unit.Luck - (unit.Luck / 2);
      int mainCharacterAttackValue = MainCharacterAttack(unit.Strength, unit.Luck, rnd, false);
      Debug.Log($"main character's turn. his attack: {mainCharacterAttackValue}");
      int mobAgilityToUse = Enemy.UnitSettings.Agility;
      if (mobAgilityToUse >= 8)
      {
        mobAgilityToUse = 10;
      }
      else
      {
        mobAgilityToUse += 2;
      }
      if (ReceiveDamage(mobAgilityToUse, Enemy.UnitSettings, Enemy.UnitSettings.Hp, rnd, mainCharacterAttackValue, true)) return;
      Debug.Log("mob was defeated. death");
    }
      
    private int MainCharacterAttack(int strength, int luck, Random rnd, bool toHead)
    {
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
    
    private static void MobTurn(Random rnd)
    {
      int mobAttackValue = MobAttack(Enemy.UnitSettings, rnd);
      Debug.Log($"mob's turn. his attack: {mobAttackValue}");
      if (ReceiveDamage(Game.Instance.Settings.Agility, Game.Instance.Settings, Game.Instance.CurrentHealth, rnd, mobAttackValue, false)) return;
      Debug.Log("main char was defeated");
    }

    private static int MobAttack(UnitSettings unit, Random rnd)
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

    private static bool ReceiveDamage(int agilityToUse, UnitSettings unit, int opponentHp, Random rnd, int damage, bool toLegs)
    {
      int dodgeProbability = (agilityToUse * 3);
      int cubeParameter = rnd.Next(1, 101);
      
      // successfully dodged
      if (cubeParameter >= 1 && cubeParameter < dodgeProbability)
      {
        Debug.Log("opponent dodged");
        return true;
      }

      Debug.Log($"opponent haven't dodged and got attacked. hp before attach: {opponentHp}");
      opponentHp -= damage;
      Debug.Log($"his hp after: {opponentHp}");
      if (toLegs)
      {
        unit.Agility -= (unit.Agility / 2);
      }
      return opponentHp > 0;
    }
  }
}