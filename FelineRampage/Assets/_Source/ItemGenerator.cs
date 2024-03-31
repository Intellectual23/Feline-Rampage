using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
  public static ItemGenerator Instance;
  [SerializeField] private GameObject _itemPrefab;
  public List<ItemAsset> _items = new();
  private List<Vector3> _spawnPositions = new();

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
    RectTransform parentPosition = Game.Instance.CurrentRoom.GetComponent<RectTransform>();
    foreach (Transform childTransform in parentPosition.transform)
    {
      Vector3 positionRelativeToParent = childTransform.position;
      _spawnPositions.Add(positionRelativeToParent);
    }
  }

  private void Update()
  {
    //UpdateSpawnPositions();
  }

  private void UpdateSpawnPositions()
  {
    _spawnPositions.Clear();
    RectTransform parentPosition = Game.Instance.CurrentRoom.GetComponent<RectTransform>();
    foreach (Transform childTransform in parentPosition.transform)
    {
      Vector3 positionRelativeToParent = childTransform.position;
      _spawnPositions.Add(positionRelativeToParent);
    }
  }

  public void GenerateItems()
  {
    UpdateSpawnPositions();
    int genValue = UnityEngine.Random.Range(1,101);
    int luck = Game.Instance.Settings.Luck;
    int epicChance = 5 + 5 * luck;
    int rareChance = 20 + 5 * luck;
    int setType = UnityEngine.Random.Range(1, 4);
    if (genValue <= epicChance)
    {
      GenerateEpicSet();
    }
    else if (genValue <= epicChance + rareChance)
    {
      GenerateRareSet();
    }
    else
    {
      GenerateCommonSet();
    }
  }

  public void GenerateEnemyDrop(Vector3 position)
  {
    int spawn = UnityEngine.Random.Range(0, 2);
    var consumableAssets = GetConsumableAssets();
    if (spawn == 0)
    {
      SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)], ItemStatus.Default), position);
    }
  }

  public void GenerateShopItems()
  {
    UpdateSpawnPositions();
    var epicAssets = GetEpicAssets();
    var rareAssets = GetRareAssets();
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int luck = Game.Instance.Settings.Luck;
    if (luck < 3)
    {
      SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Shop), _spawnPositions[0]);
      SpawnItem(new Artifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], ItemStatus.Shop), _spawnPositions[1]);
      SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Shop), _spawnPositions[2]);
    }
    else if (luck < 5)
    {
      SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Shop), _spawnPositions[0]);
      SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Shop), _spawnPositions[1]);
      SpawnItem(new Artifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], ItemStatus.Shop), _spawnPositions[2]);
      SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Shop), _spawnPositions[3]);
      SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Shop), _spawnPositions[4]);
    }
    else if (luck == 5)
    {
      SpawnItem(new Artifact(epicAssets[UnityEngine.Random.Range(0, epicAssets.Count)],ItemStatus.Shop), _spawnPositions[0]);
      SpawnItem(new Artifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], ItemStatus.Shop), _spawnPositions[1]);
      SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Shop), _spawnPositions[2]);
      SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Shop), _spawnPositions[3]);
      SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Shop), _spawnPositions[4]);
    }

  }

  public void SpawnToInventory(Item.Item item)
  {
    GameObject itemObject = Instantiate(_itemPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
    int index = Inventory.Instance.Items.Count;
    Inventory.Instance.Add(itemView);
  }

  private void GenerateCommonSet()
  {
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int setVar = UnityEngine.Random.Range(1, 3);
    Debug.Log("COUNT:" + commonAssets.Count);
    switch (setVar)
    {
      case 1:
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[0]);
        break;
      case 2:
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[0]);
        SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Default), _spawnPositions[1]);
        break;
    }
  }

  private void GenerateRareSet()
  {
    var rareAssets = GetRareAssets();
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int setVar = UnityEngine.Random.Range(1, 4);
    switch (setVar)
    {
      case 1:
        SpawnItem(new Artifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], ItemStatus.Default), _spawnPositions[0]);
        break;
      case 2:
        SpawnItem(new Artifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], ItemStatus.Default), _spawnPositions[0]);
        SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Default), _spawnPositions[1]);
        break;
      case 3:
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[0]);
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[1]);
        break;
    }
  }

  private void GenerateEpicSet()
  {
    var epicAssets = GetEpicAssets();
    var rareAssets = GetRareAssets();
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int setVar = UnityEngine.Random.Range(1, 5);
    switch (setVar)
    {
      case 1:
        SpawnItem(new Artifact(epicAssets[UnityEngine.Random.Range(0, epicAssets.Count)],ItemStatus.Default), _spawnPositions[0]);
        break;
      case 2:
        SpawnItem(new Artifact(epicAssets[UnityEngine.Random.Range(0, epicAssets.Count)],ItemStatus.Default), _spawnPositions[0]);
        SpawnItem(new Consumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)],ItemStatus.Default), _spawnPositions[1]);
        break;
      case 3:
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[0]);
        SpawnItem(new Artifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], ItemStatus.Default), _spawnPositions[1]);
        break;
      case 4:
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[0]);
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[1]);
        SpawnItem(new Artifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)],ItemStatus.Default), _spawnPositions[2]);
        break;
    }
  }

  private List<ItemAsset> GetConsumableAssets() =>
    _items.Where(asset => asset.Rarity == 0).Select(thisItem => thisItem).OrderBy(item => item.Id).ToList();

  private List<ItemAsset> GetCommonAssets() =>
    _items.Where(asset => asset.Rarity == 1).Select(thisItem => thisItem).OrderBy(item => item.Id).ToList();

  private List<ItemAsset> GetRareAssets() =>
    _items.Where(asset => asset.Rarity == 2).Select(thisItem => thisItem).OrderBy(item => item.Id).ToList();

  private List<ItemAsset> GetEpicAssets() =>
    _items.Where(asset => asset.Rarity == 3).Select(thisItem => thisItem).OrderBy(item => item.Id).ToList();

  private void SpawnItem(Item.Item item, Vector3 position)
  {
    GameObject itemObject = Instantiate(_itemPrefab, position, Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
  }
}