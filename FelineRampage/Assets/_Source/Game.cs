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
  public List<UnitSettings> _units = new();
  public int CoinBalance = 0;
  [SerializeField] private GameObject _unitPrefab;

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
    //SpawnItem(0);
    SpawnUnit(0);
  }

  void SpawnItem(int index)
  {
    
  }

  void SpawnUnit(int index)
  {
    var unit = new Unit.Unit(_units[index]);
    GameObject unitObject = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
    UnitView unitView = unitObject.GetComponent<UnitView>();
    unitView.Init(unit);
  }

  // Update is called once per frame
  void Update()
  {
  }
}