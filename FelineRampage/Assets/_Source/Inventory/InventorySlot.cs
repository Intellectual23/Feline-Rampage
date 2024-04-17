using Item;
using Unity.VisualScripting;
using UnityEngine;

  public class InventorySlot : MonoBehaviour
  {
    public ItemView Item { get; set; }
    public bool IsFilled { get; set; }
    
  }