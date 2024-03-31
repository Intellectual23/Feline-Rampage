using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
  public class ColliderHandler : MonoBehaviour
  {
    [SerializeField] private string _part;
    private Unit _unit;
    
    void OnMouseOver()
    {
      if (Input.GetMouseButtonDown(1))
      {
        OnMouseDown();
      }
    }
    
    void OnMouseDown()
    {
      //hit of Main Char
      Debug.Log(_part);
      gameObject.transform.parent.transform.GetComponent<UnitView>()._fightMode.Part = _part;
    }
  }
}

