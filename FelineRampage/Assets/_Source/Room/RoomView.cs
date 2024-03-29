﻿using System;
using UnityEngine;

namespace Room
{
  public class RoomView : MonoBehaviour
  {
    public string _name;
    public bool _hasLeftPath;
    public bool _hasRightPath;
    public bool _hasFrontPath;
    public bool _hasBackPath;
    public RoomType _roomType;
    

    private void Start()
    {
      RectTransform roomRectTransform = GetComponent<RectTransform>();
      foreach (Transform childTransform in transform)
      {
        RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
        Vector3 positionRelativeToParent = childRectTransform.anchoredPosition;
        Debug.Log("Child position relative to parent: " + positionRelativeToParent);
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("MainCamera"))
      {
        Game.Instance.CurrentRoom = this;
        RoomActivity();
      }
    }

    private void RoomActivity()
    {
      switch (_roomType)
      {
        case RoomType.Shop:
          //ItemGenerator.Instance.GenerateShopItems();
          break;
        case RoomType.BasicRoom:
          UnitGenerator.Instance.MobsOrEmpty();
          break;
        case RoomType.Treasures:
          ItemGenerator.Instance.GenerateItems();
          break;
        case RoomType.Boss:
          UnitGenerator.Instance.SpawnBoss();
          break;
        case RoomType.StartRoom:
          // show the rules of the game
          break;
        case RoomType.Deadend:
          break;
        default:
          Debug.Log("switch in room view cannot assess the type of the room");
          break;
      }
    }
  }
}