using UnityEngine;

namespace Item
{
  public class ItemView : MonoBehaviour
  {
    public Item _item;
    public int _slotId = -1;
    public void Init(Item item)
    {
      _item = item;
      Transform image = transform.GetChild(0);
      if (image == null) return;
      image.GetComponent<SpriteRenderer>().sprite = item.ItemAsset.Icon;
    }

    public void Update()
    {
      if (_item._itemStatus == ItemStatus.Inventory)
      {
        gameObject.SetActive(Inventory.Instance.IsActive);
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        transform.position = Inventory.Instance.Slots[_slotId].transform.position;
      }
    }
    private void OnMouseDown()
    {
      if (_item._itemStatus == ItemStatus.Shop)
      {
        int cost = Game.Instance.BasicArtifactCost * _item.ItemAsset.Rarity + Game.Instance.BasicConsumableCost;
        if (Game.Instance.CoinBalance >= cost)
        {
          Game.Instance.CoinBalance -= cost;
          Inventory.Instance._textLine.text =  $"- {_item.ItemAsset.Name} is bought!";
          MoveToInventory();
        }
      }
      else if (_item._itemStatus == ItemStatus.Default)
      {
        MoveToInventory();
      }
      else
      {
        if (_item.GetType() == typeof(Consumable))
        {
          Consumable item = _item as Consumable;
          item?.Use();
          Inventory.Instance.DeleteFromSlot(_slotId);
          Destroy(gameObject);
        }
       Inventory.Instance._textLine.text = _item.ItemAsset.Description;
      }
    }

    private void MoveToInventory()
    {
      _item._itemStatus = ItemStatus.Inventory;
      _item.Collect();
      ItemGenerator.Instance.SpawnToInventory(_item);
      Destroy(gameObject);
    }
  }
}