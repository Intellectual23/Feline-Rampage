using UnityEngine;

namespace Item
{
  public class Artifact: Item
  {
    public Artifact(ItemAsset asset): base(asset) { }
    public override void Collect()
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
  }
}