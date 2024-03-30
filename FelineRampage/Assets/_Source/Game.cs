using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using Room;
using Unit;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Game : MonoBehaviour
{
  public static Game Instance;
  [SerializeField]public int LvlId;
  [SerializeField]public int BasicArtifactCost;
  [SerializeField]public int BasicConsumableCost;
  [SerializeField] public int MinCoinsFromEnemy;
  [SerializeField] public int MaxCoinsFromEnemy;
  public int CurrentHealth { get; set; }
  [SerializeField] private UnitSettings StartSettings;
  [SerializeField] public UnitSettings Settings;
  public int CoinBalance = 0;
  public RoomView CurrentRoom;

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
    LoadStats();
   
    //s.LoadGameData();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S))
    {
      Serializer s = new Serializer();
      s.SaveGameData();
      Debug.Log("SAVE");
    }
    if (Input.GetKeyDown(KeyCode.L))
    {
      Serializer s = new Serializer();
      s.LoadGameData();
      Debug.Log("LOAD");
    }
  }

  private void LoadStats()
  {
    Settings.Hp = StartSettings.Hp;
    CurrentHealth = Settings.Hp;
    Settings.Strength = StartSettings.Strength;
    Settings.Agility = StartSettings.Agility;
    Settings.Luck = StartSettings.Luck;
    Settings.Icon = StartSettings.Icon;
  }
}