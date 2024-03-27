using System;
using System.Collections.Generic;
using Item;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public static Inventory Instance;
  public bool IsActive { get; set; } = false;
  public int Count { get; set; } = 0;
  

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
    gameObject.SetActive(false);
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

  public void AddToSlot(int index, GameObject item)
  {
    var slot = transform.GetChild(index).GetComponent<InventorySlot>();
    slot.Item = item;
    slot.IsFilled = true;
  }

  public void DeleteFromSlot(int index)
  {
    var slot = transform.GetChild(index).GetComponent<InventorySlot>();
    slot.Item = null;
    slot.IsFilled = false;
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