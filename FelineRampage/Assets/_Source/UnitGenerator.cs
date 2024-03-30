using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using UnityEngine.UI;

// interface button deactivates button if 
// unitgenerator's isMobHere == true; isMobHere depends on
// unitview's update with its destoy(gameObject)

public class UnitGenerator: MonoBehaviour
{
  public static UnitGenerator Instance;
  // rat goblin skeleton
  public List<UnitSettings> _units = new();
  public List<GameObject> _unitPrefabs = new();
  private List<Vector3> _spawnPositions = new();
  public UnitSettings _bossAsset;
  public GameObject _bossPrefab;
    
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

  public void SpawnBoss()
  {
    UpdateSpawnPositions();
    var boss = new Unit.Unit(_bossAsset);
    GameObject unitObject = Instantiate(_bossPrefab, _spawnPositions[1], Quaternion.identity);
    UnitView unitView = unitObject.GetComponent<UnitView>();
    unitView.Init(boss);
  }

  public int MobsOrEmpty()
  {
    int curProbability = UnityEngine.Random.Range(1, 101);
    int emptinessProbability = 20;
    // 1-emptinessProbability => empty
    // emptinessProbability+1 - 100 => mobs
    if (curProbability >= emptinessProbability+1 && curProbability <= 100)
    {
      return GenerateMobs();
    }

    return 0;
  }

  int GenerateMobs()
  {
    // рандом числа генерации
    int amountOfMobs = UnityEngine.Random.Range(0, 3) + 1;
    Debug.Log($"amount of enemies: {amountOfMobs}");
    UpdateSpawnPositions();
    // колво мобов для спавна
    for (int i = 1; i <= amountOfMobs; ++i)
    {
      int assetNumber = UnityEngine.Random.Range(0, 3);
      Debug.Log(_units[assetNumber].name);
      SpawnMobs(assetNumber, _spawnPositions[i - 1]);
    }

    return amountOfMobs;
  }

  private void SpawnMobs(int assetNumber, Vector3 position)
  {
    Debug.Log("spawn mob");
    var mob = new Unit.Unit(_units[assetNumber]);
    GameObject unitObject = Instantiate(_unitPrefabs[assetNumber], position, Quaternion.identity);
    UnitView unitView = unitObject.GetComponent<UnitView>();
    unitView.Init(mob);
    
    //unitObject.transform.GetChild(4).GetComponent<Canvas>().transform.GetChild(0).transform.GetComponent<Slider>() = unitView.HealthBar.healthSlider;
  }
}