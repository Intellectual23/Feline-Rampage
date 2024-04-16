using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

namespace Room
{
  public class RoomView : MonoBehaviour
  {
    public string _name;
    public bool _hasLeftPath;
    public bool _hasRightPath;
    public bool _hasFrontPath;
    public bool _hasBackPath;
    public RoomType _roomType;
    public bool _isActive = true;
    //private bool _wasActivated = false;
    public bool _hasFrontPathInitially;
    public int _numberOfMobsHere = 0;
    public int _id;
    public bool _isShop;
    public bool _isTreasures;
    public bool _isBoss;
    public bool _isDeadEnd;
    public int _spawnPointAmount;

    private void Start()
    {
      _hasFrontPathInitially = _hasFrontPath;
      RectTransform roomRectTransform = GetComponent<RectTransform>();
      foreach (Transform childTransform in transform)
      {
        RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
        Vector3 positionRelativeToParent = childRectTransform.anchoredPosition;
        // Debug.Log("Child position relative to parent: " + positionRelativeToParent);
      }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("MainCamera"))
      {
        Game.Instance.CurrentRoom = this;
        if (_isActive)
        {
          Debug.Log("is active and was not activated");
          RoomActivity();
          _isActive = false;
        }
        // Debug.Log(name);
      }
      Debug.Log($"{other.name} = other. on trigger");

      if (other.transform.GetComponent<RoomView>()._roomType == RoomType.StartRoom)
      {
        return;
      }

      if (other.CompareTag("Room"))
      {
        Debug.Log("room was deleted");
        RoomGenerator.Instance._map.Remove(gameObject);
        Destroy(gameObject);
      }
        
      if (other.CompareTag("ShopRoom"))
      {
        Debug.Log("shoproom was deleted");
        RoomGenerator.Instance._map.Remove(gameObject);
        Destroy(gameObject);
      }
        
      if (other.CompareTag("BossRoom"))
      {
        Debug.Log("bossroom was deleted");
        RoomGenerator.Instance._map.Remove(gameObject);
        Destroy(gameObject);
      }
        
      if (other.CompareTag("TreasuresRoom"))
      {
        Debug.Log("treasuresroom was deleted");
        RoomGenerator.Instance._map.Remove(gameObject);
        Destroy(gameObject);
      }
    }

    private void Update()
    {
      _hasFrontPath = _numberOfMobsHere == 0 && _hasFrontPathInitially;
    }

    public void RoomActivity()
    {
      Debug.Log(_roomType);
      switch (_roomType)
      {
        case RoomType.StartRoom:
          Debug.Log("start room in switch");
          // show the rules of the game
          break;
        case RoomType.Shop:
          ItemGenerator.Instance.GenerateShopItems();
          break;
        case RoomType.BasicRoom:
          Debug.Log("basic room in switch");
          _numberOfMobsHere += UnitGenerator.Instance.MobsOrEmpty();
          break;
        case RoomType.Treasures:
          ItemGenerator.Instance.GenerateItems();
          break;
        case RoomType.Boss:
          UnitGenerator.Instance.SpawnBoss();
          break;
        case RoomType.Deadend:
          break;
        default:
          Debug.Log("switch in room view cannot assess the type of the room");
          break;
      }
    }
  }
}