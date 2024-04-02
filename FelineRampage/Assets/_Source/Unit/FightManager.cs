using System;
using System.Collections;
using System.Data;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;

// TODO: ПОРЕНЕЙМИТЬ, счёты уменьшить и кодстайл

namespace Unit
{
  public class FightManager: MonoBehaviour
  {
    public Unit _enemy; // враг
    public string Part { get; set; }
    public bool playerTurn = true;
    Random rnd = new Random();
    private List<Effect> unitEffects;
    private List<Effect> mainCharEffects;
    private List<Effect> allEffects; 

    public void Init(Unit unit)
    {
      _enemy = unit;
      allEffects.Add(new PoisoningEffect("poisoned", 2, 6));
      allEffects.Add(new BleedingEffect("bleeding", 4, 3));
      allEffects.Add(new StunEffect("stun", 0, 3));
    }
    public void StartFight()
    {
      StartCoroutine(FightMode());
      Debug.Log("StartFight");
    }
    public IEnumerator FightMode()
    {
      yield break;
    }
  }
}