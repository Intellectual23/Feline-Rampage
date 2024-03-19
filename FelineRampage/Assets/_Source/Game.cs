using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using Unit;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{
  public static Game Instance;
  public UnitSettings Settings;
  public List<ItemAsset> _items = new();
  public List<UnitSettings> _units = new();
  public int _coinBalance = 0;
  [NonSerialized] public List<ItemView> Inventory = new();
  [SerializeField] private GameObject _itemPrefab;

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

  // Start is called before the first frame update
  void Start()
  {
    SpawnItem(0);
  }

  void SpawnItem(int index)
  {
    var item = new Item.Item(_items[index]);
    GameObject itemObject = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
  }

  // Update is called once per frame
  void Update()
  {
  }
}