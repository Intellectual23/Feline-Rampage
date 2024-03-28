using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

public class UnitGenerator: MonoBehaviour
{
  public static UnitGenerator Instance;
  // rat goblin skeleton
  public List<UnitSettings> _units = new();
  public List<GameObject> _unitPrefabs = new();
  private List<Vector3> _spawnPositions = new();
  private bool isMobsSpawned = false;
    
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
    Debug.Log("start");
    RectTransform parentPosition = Game.Instance.CurrentRoom.GetComponent<RectTransform>();
    foreach (Transform childTransform in parentPosition.transform)
    {
      Vector3 positionRelativeToParent = childTransform.localPosition;
      _spawnPositions.Add(positionRelativeToParent);
    }
    GenerateMobs();
  }

  private void Update()
  {
    RectTransform parentPosition = Game.Instance.CurrentRoom.GetComponent<RectTransform>();
    foreach (Transform childTransform in parentPosition.transform)
    {
      Vector3 positionRelativeToParent = childTransform.localPosition;
      _spawnPositions.Add(positionRelativeToParent);
    }
  }

  void GenerateMobs()
  {
    // рандом числа генерации
    int amountOfMobs = UnityEngine.Random.Range(0, 3) + 1;
    Debug.Log($"amount of enemies: {amountOfMobs}");
    // колво мобов для спавна
    for (int i = 1; i <= amountOfMobs; ++i)
    {
      int assetNumber = UnityEngine.Random.Range(0, 3);
      Debug.Log(_units[assetNumber].name);
      SpawnMobs(assetNumber, _spawnPositions[i - 1]);
    }
  }

  private void SpawnMobs(int assetNumber, Vector3 position)
  {
    Debug.Log("spawn");
    var mob = new Unit.Unit(_units[assetNumber]);
    GameObject unitObject = Instantiate(_unitPrefabs[assetNumber], position, Quaternion.identity);
    UnitView itemView = unitObject.GetComponent<UnitView>();
    Debug.Log(itemView.name);
    Debug.Log(mob.UnitSettings.name);
    itemView.Init(mob);
  }
}