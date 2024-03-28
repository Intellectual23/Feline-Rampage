using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace Item
{
  public class ItemView : MonoBehaviour
  {
    private Item _item;
    public int _slotId = -1;
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
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
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
        Destroy(gameObject);
      }
      else
      {
        if (_item.GetType() == typeof(Consumable))
        {
          Consumable item = _item as Consumable;
          item?.Use();
          Inventory.Instance.DeleteFromSlot(_slotId);
          Destroy(gameObject);
        }
        Debug.Log(_item.ItemAsset.Description);
      }
      //Inventory.Instance.Items.Add(this);
    }
  }
}