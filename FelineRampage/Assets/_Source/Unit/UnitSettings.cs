using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
  [CreateAssetMenu(menuName = "SO/new Unit Asset", fileName = "UnitAsset")]
  public class UnitSettings : ScriptableObject
  {
    [field: SerializeField] public int Hp { get; set; }
    [field: SerializeField] public int Strength { get; set; }
    [field: SerializeField] public int Agility { get; set; }
    [field: SerializeField] public int Luck { get; set; }
    [field: SerializeField] public Sprite Icon { get; set; }
  }
}