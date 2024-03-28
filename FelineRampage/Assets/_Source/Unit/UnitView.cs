using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Unit
{
  public class UnitView : MonoBehaviour
  {
    private Unit _unit;
    private int _startHealth;

    public void Init(Unit unit)
    {
      _unit = unit;
      Transform image = transform.GetChild(3);
      if (image == null) return;
      image.GetComponent<SpriteRenderer>().sprite = unit.UnitSettings.Icon;
    }

    public Unit Unit()
    {
      return _unit;

    }
    public void Start()
    {
      _startHealth = _unit.UnitSettings.Hp;
    }

    public void Update()
    {
      if (_unit.UnitSettings.Hp <= 0)
      {
        Debug.Log("update found defeated enemy");
        Destroy(gameObject);
        ResetHealth();
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
        FightManager.Enemy = _unit;
        EnableColliders();
        _unit.IsFighting = true;
        StartCoroutine(FightManager.Instance.FightMode());
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