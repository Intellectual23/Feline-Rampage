using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Item
{
  [Serializable]
  public abstract class Item
  {
    [SerializeField]
    protected ItemAsset _itemAsset;
    [SerializeField]
    public ItemStatus _itemStatus;
    public Item(ItemAsset itemAsset, ItemStatus itemStatus)
    {
      _itemStatus = itemStatus;
      _itemAsset = itemAsset;
    }

    public abstract void Collect();
    
    public ItemAsset ItemAsset
    {
      get => _itemAsset;
      set => _itemAsset = value;
    }
  }
}