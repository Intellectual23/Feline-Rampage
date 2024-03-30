using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
  [CreateAssetMenu(menuName = "SO/new Item Asset", fileName = "ItemAsset")]
  [Serializable]
  public class ItemAsset : ScriptableObject
  {
    [field: SerializeField] public int Id { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public string Description { get; set; }
    [field: SerializeField] public int Rarity { get; set; }
    [field: SerializeField] public Sprite Icon { get; set; }
    [field: SerializeField] public GameStat StatToBuff { get; set; }
    [field: SerializeField] public int BuffValue { get; set; }
  }
}