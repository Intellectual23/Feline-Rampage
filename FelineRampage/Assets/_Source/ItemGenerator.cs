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
      Vector3 positionRelativeToParent = childTransform.localPosition;
      _spawnPositions.Add(positionRelativeToParent);
    }

    //GenerateEnemyDrop();
    GenerateItems();
  }

  public void GenerateItems()
  {
    int setType = UnityEngine.Random.Range(1, 4);
    Debug.Log(setType);
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
    int q = UnityEngine.Random.Range(0, 4);
    var consumableAssets = GetConsumableAssets();
    for (int i = 0; i < q; ++i)
    {
      SpawnConsumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)], _spawnPositions[i]);
    }
  }

  public void SpawnToInventory(Item.Item item, Vector3 position)
  {
    GameObject itemObject = Instantiate(_itemPrefab, position, Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
    Inventory.Instance.AddToSlot(Inventory.Instance.Count, itemObject);
    itemView._slotId = Inventory.Instance.Count;
    Inventory.Instance.Count++;
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
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[0]);
        break;
      case 2:
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[0]);
        SpawnConsumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)], _spawnPositions[1]);
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
        SpawnArtifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], _spawnPositions[0]);
        break;
      case 2:
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[0]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[1]);
        break;
      case 3:
        SpawnArtifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], _spawnPositions[0]);
        SpawnConsumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)], _spawnPositions[1]);
        break;
    }
  }

  private void GenerateEpicSet() // Can be simplier!
  {
    var epicAssets = GetEpicAssets();
    var rareAssets = GetRareAssets();
    var commonAssets = GetCommonAssets();
    var consumableAssets = GetConsumableAssets();
    int setVar = UnityEngine.Random.Range(1, 5);
    switch (setVar)
    {
      case 1:
        SpawnArtifact(epicAssets[UnityEngine.Random.Range(0, epicAssets.Count)], _spawnPositions[0]);
        break;
      case 2:
        SpawnArtifact(epicAssets[UnityEngine.Random.Range(0, epicAssets.Count)], _spawnPositions[0]);
        SpawnConsumable(consumableAssets[UnityEngine.Random.Range(0, consumableAssets.Count)], _spawnPositions[1]);
        break;
      case 3:
        SpawnArtifact(rareAssets[UnityEngine.Random.Range(0, rareAssets.Count)], _spawnPositions[0]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[1]);
        break;
      case 4:
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[0]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[1]);
        SpawnArtifact(commonAssets[UnityEngine.Random.Range(0, commonAssets.Count)], _spawnPositions[2]);
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

  private void SpawnArtifact(ItemAsset itemAsset, Vector3 position) // add position or layout 
  {
    var item = new Artifact(itemAsset, false);
    GameObject itemObject = Instantiate(_itemPrefab, position, Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
    //Game.Instance.Items.Add(item, itemView);
  }

  private void SpawnConsumable(ItemAsset itemAsset, Vector3 position) // add position or layout
  {
    var item = new Consumable(itemAsset, false);
    GameObject itemObject = Instantiate(_itemPrefab, position, Quaternion.identity);
    ItemView itemView = itemObject.GetComponent<ItemView>();
    itemView.Init(item);
    //Game.Instance.Items.Add(item, itemView);
  }
}