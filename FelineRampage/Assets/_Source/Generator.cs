﻿using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class Generator : MonoBehaviour
{
  [SerializeField] private GameObject _itemPrefab;
  public List<ItemAsset> _items = new();

  public void GenerateItems()
  {
    int setType = UnityEngine.Random.Range(1, 3);
    switch (setType)
    {
      case 1:
        GenerateCommonSet();
        break;
      case 2:
        GenerateRareSet();
        break;
      case 3:
        GenerateEpicSet();
        break;
    }
  }

  public void GenerateEnemyDrop()
  {
    int q = UnityEngine.Random.Range(0, 3);
    for (int i = 0; i < q; ++i)
    {
      SpawnConsumable(_items[UnityEngine.Random.Range(0, _items.Count - 1)]);
    }
  }

  private void GenerateCommonSet()
  {
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int setVar = UnityEngine.Random.Range(1, 2);
    switch (setVar)
    {
      case 1:
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        break;
      case 2:
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        SpawnConsumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count - 1)]);
        break;
    }
  }

  private void GenerateRareSet()
  {
    var rareAssets = GetRareAssets();
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int setVar = UnityEngine.Random.Range(1, 3);
    switch (setVar)
    {
      case 1:
        SpawnArtifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count - 1)]);
        break;
      case 2:
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        break;
      case 3:
        SpawnArtifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count - 1)]);
        SpawnConsumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count - 1)]);
        break;
    }
  }

  private void GenerateEpicSet()
  {
    var epicAssets = GetEpicAssets();
    var rareAssets = GetRareAssets();
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int setVar = UnityEngine.Random.Range(1, 3);
    switch (setVar)
    {
      case 1:
        SpawnArtifact(epicAssets[UnityEngine.Random.Range(0, epicAssets.Count - 1)]);
        break;
      case 2:
        SpawnArtifact(epicAssets[UnityEngine.Random.Range(0, epicAssets.Count - 1)]);
        SpawnConsumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count - 1)]);
        break;
      case 3:
        SpawnArtifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count - 1)]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        break;
      case 4:
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count - 1)]);
        break;
    }
  }

  private List<ItemAsset> GetConsumableAssets() =>
    _items.Where(asset => asset.Rarity == 0).Select(thisItem => thisItem).ToList();

  private List<ItemAsset> GetCommonAssets() =>
    _items.Where(asset => asset.Rarity == 1).Select(thisItem => thisItem).ToList();

  private List<ItemAsset> GetRareAssets() =>
    _items.Where(asset => asset.Rarity == 2).Select(thisItem => thisItem).ToList();

  private List<ItemAsset> GetEpicAssets() =>
    _items.Where(asset => asset.Rarity == 3).Select(thisItem => thisItem).ToList();

  private void SpawnArtifact(ItemAsset itemAsset) // add position or layout 
  {
    var item = new Artifact(itemAsset);
    GameObject itemObject = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
    //Game.Instance.Items.Add(item, itemView);
  }

  private void SpawnConsumable(ItemAsset itemAsset) // add position or layout
  {
    var item = new Consumable(itemAsset);
    GameObject itemObject = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
    //Game.Instance.Items.Add(item, itemView);
  }
}