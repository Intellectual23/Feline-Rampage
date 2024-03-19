using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
  public class ItemView : MonoBehaviour
  {
    private Item _item;

    public void Init(Item item)
    {
      _item = item;
    }
  }
}