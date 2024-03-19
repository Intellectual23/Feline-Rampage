using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Item
{
  public class ItemView : MonoBehaviour
  {
    private Item _item;

    public void Init(Item item)
    {
      _item = item;
      Transform image = transform.GetChild(0);
      if (image == null) return;
      image.GetComponent<SpriteRenderer>().sprite = item.ItemAsset.Icon;
    }

    private void OnMouseDown()
    {
      _item.Collect();
      Game.Instance.Inventory.Add(this);
      Destroy(gameObject);
    }
  }
}