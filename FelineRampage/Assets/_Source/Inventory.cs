using System;
using System.Collections.Generic;
using Item;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public static Inventory Instance;
  [NonSerialized]public List<ItemView> Items;
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
}