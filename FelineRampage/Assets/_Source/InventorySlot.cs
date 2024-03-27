using Item;
using Unity.VisualScripting;
using UnityEngine;

  public class InventorySlot : MonoBehaviour
  {
    public GameObject Item { get; set; }
    public bool IsFilled { get; set; }

    private void Update()
    {
      if (IsFilled) 
      {
        Item.SetActive(Inventory.Instance.IsActive);
        Item.transform.position = transform.position;
      }
    }
  }