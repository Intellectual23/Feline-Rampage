using UnityEngine;

public class InventoryButton : MonoBehaviour
{
  public void OnClick()
  {
    Inventory.Instance.ClickManager();
  }
}