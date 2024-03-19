using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Item
{
  public class Item
  {
    private ItemAsset _itemAsset;

    public Item(ItemAsset itemAsset)
    {
      _itemAsset = itemAsset;
    }

    public void Collect()
    {
      switch (_itemAsset.StatToBuff)
      {
        case GameStat.Hp:
          Game.Instance.Settings.Hp += _itemAsset.BuffValue;
          break;
        case GameStat.Strength:
          Game.Instance.Settings.Strength += _itemAsset.BuffValue;
          break;
        case GameStat.Agility:
          Game.Instance.Settings.Agility += _itemAsset.BuffValue;
          break;
        case GameStat.Luck:
          Game.Instance.Settings.Luck += _itemAsset.BuffValue;
          break;
      }
      Debug.Log("ITEM IS COLLECTED");
    }

    public ItemAsset ItemAsset
    {
      get => _itemAsset;
      set => _itemAsset = value;
    }
  }
}