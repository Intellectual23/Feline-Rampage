using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using Room;
using Unit;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{
  public static Game Instance;
  public int _startHealth = 100;
  public UnitSettings Settings;
  public List<UnitSettings> _units = new();
  public int CoinBalance = 0;
  public RoomView CurrentRoom;
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
  
  public void Start()
  {
    Settings.Hp = _startHealth;
  }
}