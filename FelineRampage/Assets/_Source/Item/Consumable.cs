using UnityEngine;

namespace Item
{
  public class Consumable: Item
  {
    public Consumable(ItemAsset itemAsset) : base(itemAsset) { }

    public override void Collect()
    {
      //
    }

    public void Use()
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
      Debug.Log("ITEM IS USED");
    }
  }
}