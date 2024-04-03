using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameInteface : MonoBehaviour
{
  public TextMeshProUGUI _coinBalanceText;
  public TextMeshProUGUI _strengthText;
  public TextMeshProUGUI _agilityText;
  public TextMeshProUGUI _luckText;
  
  //public Sprite _characterIcon;
  public static GameInteface Instance;

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

  public void Start()
  {
    Transform characterIcon = transform.GetChild(0).GetChild(0);
    characterIcon.GetComponent<SpriteRenderer>().sprite = Game.Instance.Settings.Icon;
  }
  public void Update()
  {
    _coinBalanceText.text = $"{Game.Instance.CoinBalance + 6}";
    _strengthText.text = $"STR: {Game.Instance.Settings.Strength}";
    _agilityText.text = $"AGL: {Game.Instance.Settings.Agility}";
    _luckText.text = $"LUCK: {Game.Instance.Settings.Luck}";

  }

  public void Pause()
  {
    transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
  }
}