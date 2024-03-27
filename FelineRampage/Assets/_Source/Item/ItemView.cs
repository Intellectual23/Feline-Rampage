using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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

    public void Update()
    {
      if (_item._isInInventory)
      {
        gameObject.SetActive(Inventory.Instance.IsActive);
      }
    }
    private void OnMouseDown()
    {
      if (!_item._isInInventory)
      {
        _item._isInInventory = true;
        _item.Collect();
        Debug.Log(Inventory.Instance.Count);
        Vector3 newPosition = Inventory.Instance.transform.GetChild(Inventory.Instance.Count).position;
        ItemGenerator.Instance.SpawnToInventory(_item, newPosition);
        Inventory.Instance.Count++;
        Destroy(gameObject);
      }
      else
      {
        Debug.Log(_item.ItemAsset.Description);
      }
      //Inventory.Instance.Items.Add(this);
    }
  }
}