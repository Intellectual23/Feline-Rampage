using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

public class UnitGenerator: MonoBehaviour
{
  public static UnitGenerator Instance;
  [SerializeField] private GameObject _unitPrefab;
  public List<UnitSettings> _units = new();
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
    GenerateMobs();
  }

  private void Unpdate()
  {
    RectTransform parentPosition = Game.Instance.CurrentRoom.GetComponent<RectTransform>();
    foreach (Transform childTransform in parentPosition.transform)
    {
      Vector3 positionRelativeToParent = childTransform.localPosition;
      _spawnPositions.Add(positionRelativeToParent);
    }
    GenerateMobs();
  }

  void GenerateMobs()
  {
    // достаём ассеты мобов
    var unitAssets = _units;
    // рандом числа генерации
    int amountOfMobs = UnityEngine.Random.Range(0, 3);
    Debug.Log($"amount of enemies: {amountOfMobs}");
    // колво крыс для спавна
    for (int i = 0; i <= amountOfMobs; ++i)
    {
      SpawnMobs(unitAssets[0], _spawnPositions[i]);
    }
  }

  private void SpawnMobs(UnitSettings unitAsset, Vector3 position)
  {
    var mob = new Unit.Unit(unitAsset);
    GameObject unitObject = Instantiate(_unitPrefab, position, Quaternion.identity);
    UnitView itemView = unitObject.GetComponent<UnitView>();
    itemView.Init(mob);
  }
}