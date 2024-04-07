using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Item;
using JetBrains.Annotations;
using Room;
using UnityEditor;
using UnityEngine;


public class Serializer
{
  private string _path = Application.persistentDataPath + "/saves/inventorySave.save";
  private string _pathMap = Application.persistentDataPath + "/saves/mapSave.save";

  public void SaveGameData()
  {
    PlayerPrefs.SetInt("CurrentHealth", Game.Instance.CurrentHealth);
    PlayerPrefs.SetInt("MaxHealth", Game.Instance.Settings.Hp);
    PlayerPrefs.SetInt("Strength", Game.Instance.Settings.Strength);
    PlayerPrefs.SetInt("Agility", Game.Instance.Settings.Agility);
    PlayerPrefs.SetInt("Luck", Game.Instance.Settings.Luck);
    PlayerPrefs.SetInt("CoinBalance", Game.Instance.CoinBalance);
    var position = GameInteface.Instance.transform.position;
    PlayerPrefs.SetFloat("xPos", position.x);
    PlayerPrefs.SetFloat("yPos", position.y);
    PlayerPrefs.SetFloat("zPos", position.z);
    SaveInventory();
    SaveMap();
  }

  public void LoadGameData()
  {
    Game.Instance.CurrentHealth = PlayerPrefs.GetInt("CurrentHealth");
    Game.Instance.Settings.Hp = PlayerPrefs.GetInt("MaxHealth");
    Game.Instance.Settings.Strength = PlayerPrefs.GetInt("Strength");
    Game.Instance.Settings.Agility = PlayerPrefs.GetInt("Agility");
    Game.Instance.Settings.Luck = PlayerPrefs.GetInt("Luck");
    Game.Instance.CoinBalance = PlayerPrefs.GetInt("CoinBalance");
    var position = new Vector3();
    position.x = PlayerPrefs.GetFloat("xPos");
    position.y = PlayerPrefs.GetFloat("yPos");
    position.z = PlayerPrefs.GetFloat("zPos");
    GameInteface.Instance.transform.position = position;
    LoadInventory();
    LoadMap();
  }

  public void SaveInventory()
  {
    List<ItemWrapper> wrappers = new();
    for (int i = 0; i < Inventory.Instance.Items.Count; ++i)
    {
      var curItem = Inventory.Instance.Items[i]._item;
      int index = curItem.ItemAsset.Id;
      wrappers.Add(new ItemWrapper(index, ItemStatus.Inventory));
    }
    var formatter = new BinaryFormatter();
    var file = File.Create(_path);
    formatter.Serialize(file,wrappers);
    file.Close();
  }

  public void LoadInventory()
  {
    var formatter = new BinaryFormatter();
    List<ItemWrapper> wrappers;
    if (File.Exists(_path))
    {
      using (FileStream fileStream = new FileStream(_path, FileMode.Open))
      {
        wrappers = (List<ItemWrapper>)formatter.Deserialize(fileStream);
      }
      foreach (var itemWrapper in wrappers)
      {
        ItemAsset itemAsset = ItemGenerator.Instance._items[itemWrapper._assetId];
        Item.Item item;
        if (itemAsset.Rarity == 0)
        {
          item = new Consumable(itemAsset, ItemStatus.Inventory);
        }
        else
        {
          item = new Artifact(itemAsset, ItemStatus.Inventory);
        }
        ItemGenerator.Instance.SpawnToInventory(item);
      }
    }
    else
    {
      Debug.Log("FILE ERROR");
    }
  }

  public void SaveMap()
  {
    List<MapWrapper> wrappers = new();
    Debug.Log(RoomGenerator.Instance._map.Count);
    foreach (GameObject room in RoomGenerator.Instance._map)
    {
      wrappers.Add(new MapWrapper(room.transform.GetComponent<RoomView>()._id, room.transform.GetComponent<RoomView>()._isActive, 
        room.transform.position.x, room.transform.position.y, room.transform.position.z));
    }

    var formatter = new BinaryFormatter();
    var file = File.Create(_pathMap);
    formatter.Serialize(file, wrappers);
    file.Close();
  }

  public void LoadMap()
  {
    var formatter = new BinaryFormatter();
    List<MapWrapper> wrappers;
    if (File.Exists(_pathMap))
    {
      using (FileStream fileStream = new FileStream(_pathMap, FileMode.Open))
      {
        wrappers = (List<MapWrapper>)formatter.Deserialize(fileStream);
      }
      RoomGenerator.Instance._map.Clear();

      foreach (MapWrapper wrapper in wrappers)
      {
        RoomGenerator.Instance.SpawnRoomAfterLoad(wrapper);
      }
    }
    else
    {
      Debug.Log("FILE ERROR");
    }
  }
}