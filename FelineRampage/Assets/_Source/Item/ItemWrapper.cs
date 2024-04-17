using System;
using UnityEngine;

namespace Item
{
  [Serializable]
  public class ItemWrapper
  {
    [SerializeField]
    public int _assetId;
    [SerializeField]
    public ItemStatus _itemStatus;
    public ItemWrapper(int assetId, ItemStatus itemStatus)
    {
      _assetId = assetId;
      _itemStatus = itemStatus;
    }
  }
}