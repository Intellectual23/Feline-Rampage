using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Unit
{
  public class UnitView : MonoBehaviour
  {
    private Unit _unit; // curr health
    private int _startHealth; // max health
    public UnitHealthBar HealthBar;

    public void Init(Unit unit)
    {
      _unit = unit;
      Transform image = transform.GetChild(3);
      if (image == null)
      {
        Debug.Log("pipa");
        return;
      }
      image.GetComponent<SpriteRenderer>().sprite = unit.UnitSettings.Icon;
    }

    public Unit Unit()
    {
      return _unit;
    }

    public void Start()
    {
      _startHealth = _unit.UnitSettings.Hp;
      HealthBar.healthSlider.maxValue = _startHealth;
      HealthBar.healthSlider.minValue = 0;
      HealthBar.healthSlider.value = _unit.UnitSettings.Hp;
    }

    public void Update()
    {
      if (HealthBar.healthSlider.value != _unit.UnitSettings.Hp)
      {
        HealthBar.healthSlider.value = _unit.UnitSettings.Hp;
      }
      
      if (_unit.UnitSettings.Hp <= 0)
      {
        Debug.Log("update found defeated enemy");
        ItemGenerator.Instance.GenerateEnemyDrop(transform.position);
        Game.Instance.CoinBalance += UnityEngine.Random.Range(Game.Instance.MinCoinsFromEnemy, Game.Instance.MaxCoinsFromEnemy + 1);
        Game.Instance.CurrentRoom._numberOfMobsHere -= 1;
        Debug.Log("object destroyed");
        //ResetHealth();
        if (gameObject.transform.CompareTag("Boss"))
        {
          Debug.Log("boss was fought");
          Game.Instance.ChangeScene();
        }
        Destroy(gameObject);
      }

      if (!_unit.IsFighting)
      {
        DisableColliders();
      }
    }

    private void OnMouseDown()
    {
      Debug.Log("on mouse down");
      Collider collider = transform.GetComponent<Collider>();
      collider.enabled = false;
      Debug.Log(collider.enabled);
      FightManager.Instance.Init(_unit);
      EnableColliders();
      _unit.IsFighting = true;
      FightManager.Instance.StartFight();
    }

    private void EnableColliders()
    {
      for (int i = 0; i < 3; ++i)
      {
        transform.GetChild(i).transform.GetComponent<Collider>().enabled = true;
      }
    }

    private void DisableColliders()
    {
      for (int i = 0; i < 3; ++i)
      {
        transform.GetChild(i).transform.GetComponent<Collider>().enabled = false;
      }
    }

    private void ResetHealth()
    {
      _unit.UnitSettings.Hp = _startHealth;
    }
  }
}