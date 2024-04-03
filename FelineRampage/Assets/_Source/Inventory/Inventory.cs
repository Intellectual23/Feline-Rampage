using System;
using System.Collections.Generic;
using Item;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public static Inventory Instance;
  public bool IsActive { get; set; } = false;
  public List<ItemView> Items;
  public List<InventorySlot> Slots;
  public TextMeshProUGUI _textLine;
  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(this);
  }

  private void Start()
  {
    _textLine.text = "";
    foreach (Transform childTransform in transform)
    {
      if(childTransform.GetComponent<InventorySlot>() != null)
        Slots.Add(childTransform.transform.GetComponent<InventorySlot>());
    }
    gameObject.SetActive(false);
  }

  private void Update()
  {
    foreach (ItemView item in Items)
    {
      item.gameObject.SetActive(IsActive);
    }
  }

  public void RecalculateItems()
  {
    for (int i = 0; i < Items.Count; ++i)
    {
      
      Items[i]._slotId = i;
      Slots[i].Item = Items[i];
      Items[i].transform.position = Slots[i].transform.position;
      Slots[i].IsFilled = true;
      Debug.Log(Items.Count);
      Debug.Log(Slots.Count);
    }
  }
  public void Open()
  {
    IsActive = true;
    gameObject.SetActive(true);
  }

  public void Close()
  {
    IsActive = false;
    gameObject.SetActive(false);
  }

  public void Add(ItemView item)
  {
    item._slotId = Items.Count;
    Items.Add(item);
    var slot = transform.GetChild(item._slotId).GetComponent<InventorySlot>();
    slot.Item = item;
    slot.IsFilled = true;
  }

  public void DeleteFromSlot(int index)
  {
    var slot = transform.GetChild(index).GetComponent<InventorySlot>();
    Items.Remove(slot.Item);
    slot.Item = null;
    slot.IsFilled = false;
    RecalculateItems();
  } 

  public void ClickManager()
  {
    if (IsActive)
    {
      Close();
    }
    else
    {
      Open();
    }
  }
}