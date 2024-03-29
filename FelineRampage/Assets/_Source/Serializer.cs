using UnityEngine;


  public class Serializer 
  {
    public void SaveGameData()
    {
      PlayerPrefs.SetInt("CurrentHealth", Game.Instance.CurrentHealth);
      PlayerPrefs.SetInt("MaxHealth", Game.Instance.Settings.Hp);
      PlayerPrefs.SetInt("Strength", Game.Instance.Settings.Strength);
      PlayerPrefs.SetInt("Agility", Game.Instance.Settings.Agility);
      PlayerPrefs.SetInt("Luck", Game.Instance.Settings.Luck);
      PlayerPrefs.SetInt("CoinBalance", Game.Instance.CoinBalance);
    }

    public void LoadGameData()
    {
      Game.Instance.CurrentHealth = PlayerPrefs.GetInt("CurrentHealth");
      Game.Instance.Settings.Hp = PlayerPrefs.GetInt("MaxHealth");
      Game.Instance.Settings.Strength = PlayerPrefs.GetInt("Strength");
      Game.Instance.Settings.Agility = PlayerPrefs.GetInt("Agility");
      Game.Instance.Settings.Luck = PlayerPrefs.GetInt("Luck");
      Game.Instance.CoinBalance = PlayerPrefs.GetInt("CoinBalance");
    }
  }