using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
  public class ColliderHandler : MonoBehaviour
  {
    [SerializeField] private String _part;
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
      FightManager.Part = _part;
    }
  }
}

