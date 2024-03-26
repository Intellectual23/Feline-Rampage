using UnityEngine;
using TMPro;

public class GameInteface : MonoBehaviour
{
  public TextMeshProUGUI _coinBalanceText;
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

    DontDestroyOnLoad(this);
  }

  public void Update()
  {
    _coinBalanceText.text = $"{Game.Instance.CoinBalance + 5}";
  }
}