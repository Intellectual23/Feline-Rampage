using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Item
{
  public abstract class Item
  {
    protected ItemAsset _itemAsset;
    public bool _isInInventory;
    public Item(ItemAsset itemAsset, bool isInInventory)
    {
      _isInInventory = isInInventory;
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