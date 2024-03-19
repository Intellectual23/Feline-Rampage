using System.Collections;
using System.Collections.Generic;
using Item;
using Unit;
using UnityEngine;

public class Game : MonoBehaviour
{
  public static Game Instance;
  public UnitSettings Settings;
  public List<ItemAsset> _items = new();
  public List<UnitSettings> _units = new();
  public int CoinBalance { get; set; }
  private List<ItemView> _inventory;

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
  }

  // Update is called once per frame
  void Update()
  {
  }
}