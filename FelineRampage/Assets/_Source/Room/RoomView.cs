using System;
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

    private void Start()
    {
      RectTransform roomRectTransform = GetComponent<RectTransform>();
      foreach (Transform childTransform in transform) {
        RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
        Vector2 positionRelativeToParent = childRectTransform.anchoredPosition;
        Debug.Log("Child position relative to parent: " + positionRelativeToParent);
      }
      
    }
  }
}