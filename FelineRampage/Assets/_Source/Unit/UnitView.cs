using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Unit
{
  public class UnitView : MonoBehaviour
  {
    private Unit _unit;
    private int _startHealth;

    public void Init(Unit unit)
    {
      _unit = unit;
      Transform image = transform.GetChild(0);
      if (image == null) return;
      image.GetComponent<SpriteRenderer>().sprite = unit.UnitSettings.Icon;
    }

    private void OnMouseDown()
    {
      _unit.FightMode();
    }

    public void Start()
    {
      _startHealth = _unit.UnitSettings.Hp;
    }

    public void Update()
    {
      if (_unit.UnitSettings.Hp <= 0)
      {
        Destroy(gameObject);
        ResetHealth();
      }
    }

    private void ResetHealth()
    {
      _unit.UnitSettings.Hp = _startHealth;
    }
  }
}