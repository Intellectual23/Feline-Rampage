using System;
using UnityEngine;
using Interface;

namespace Item
{
  [Serializable]
  public class Consumable : Item
  {
    public Consumable(ItemAsset itemAsset, ItemStatus itemStatus) : base(itemAsset, itemStatus)
    {
    }

    public override void Collect()
    {
      InterfaceLog.Instance.AddMessage($"- {_itemAsset.Name} is collected");
    }

    public void Use()
    {
      switch (_itemAsset.StatToBuff)
      {
        case GameStat.Hp:
          Game.Instance.Settings.Hp += _itemAsset.BuffValue;
          if (Game.Instance.CurrentHealth + _itemAsset.BuffValue > Game.Instance.Settings.Hp)
          {
            Game.Instance.CurrentHealth = Game.Instance.Settings.Hp;
          }
          else
          {
            Game.Instance.CurrentHealth += _itemAsset.BuffValue;
          }

          break;
      }

      InterfaceLog.Instance.AddMessage($"- {_itemAsset.Name} is used");
    }
  }
}