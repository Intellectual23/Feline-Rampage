using Room;
using Unit;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
  public static Game Instance;
  [SerializeField] public int LvlId;
  [SerializeField] public int BasicArtifactCost;
  [SerializeField] public int BasicConsumableCost;
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
  }

  public void Update()
  {
    if (CurrentHealth <= 0)
    {
      PlayerPrefs.SetInt("Load", 0);
      SceneManager.LoadScene("MainMenuScene");
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    LoadStats();
    if (PlayerPrefs.GetInt("Load") == 1)
    {
      Serializer s = new Serializer();
      s.LoadGameData();
    }

    //s.LoadGameData();
    CurrentRoom.RoomActivity();
  }
  
  public void ChangeScene()
  {
    Debug.Log("change scene func");
    Debug.Log(LvlId);
    if (Game.Instance.LvlId == 1)
    {
      Debug.Log("to lvl2 scene");
      SceneManager.LoadScene("lvl2");
    } else if (Game.Instance.LvlId == 2)
    {
      Debug.Log("to lvl3 scene");
      SceneManager.LoadScene("lvl3");
    }
    else
    {
      //SceneManager.LoadScene("finalTitre");
    }
  }

  // Update is called once per frame

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