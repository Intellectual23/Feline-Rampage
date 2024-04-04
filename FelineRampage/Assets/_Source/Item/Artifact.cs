using System;
using Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace Item
{
  [Serializable]
  public class Artifact : Item
  {
    public Artifact(ItemAsset asset, ItemStatus itemStatus) : base(asset, itemStatus)
    {
    }

    public override void Collect()
    {
      switch (_itemAsset.StatToBuff)
      {
        case GameStat.Hp:
          Game.Instance.Settings.Hp += _itemAsset.BuffValue;
          Game.Instance.CurrentHealth = Game.Instance.Settings.Hp;
          break;
        case GameStat.Strength:
          Game.Instance.Settings.Strength += _itemAsset.BuffValue;
          if (Game.Instance.Settings.Strength > 10)
          {
            Game.Instance.Settings.Strength = 10;
          }

          break;
        case GameStat.Agility:
          Game.Instance.Settings.Agility += _itemAsset.BuffValue;
          if (Game.Instance.Settings.Agility > 10)
          {
            Game.Instance.Settings.Agility = 10;
          }

          break;
        case GameStat.Luck:
          Game.Instance.Settings.Luck += _itemAsset.BuffValue;
          if (Game.Instance.Settings.Luck > 5)
          {
            Game.Instance.Settings.Luck = 5;
          }

          break;
      }

      InterfaceLog.Instance.AddMessage($"- {_itemAsset.Name} is collected");
    }
  }
}