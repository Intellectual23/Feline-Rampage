using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceButton : MonoBehaviour
{
  [SerializeField] private Vector3 _moveCoordinates;
  [SerializeField] private String _direction;

  public void Update()
  {
    CheckButtons();
  }

  private void CheckButtons()
  {
    switch (_direction)
    {
      case "Front":
        if (!Game.Instance.CurrentRoom._hasFrontPath)
        {
          transform.GetComponent<Button>().interactable = false;
        }
        else
        {
          transform.GetComponent<Button>().interactable = true;
        }

        break;
      case "Back":
        if (!Game.Instance.CurrentRoom._hasBackPath)
        {
          transform.GetComponent<Button>().interactable = false;
        }
        else
        {
          transform.GetComponent<Button>().interactable = true;
        }

        break;
      case "Left":
        if (!Game.Instance.CurrentRoom._hasLeftPath)
        {
          transform.GetComponent<Button>().interactable = false;
        }
        else
        {
          transform.GetComponent<Button>().interactable = true;
        }

        break;
      case "Right":
        if (!Game.Instance.CurrentRoom._hasRightPath)
        {
          transform.GetComponent<Button>().interactable = false;
        }
        else
        {
          transform.GetComponent<Button>().interactable = true;
        }

        break;
    }
  }

  public void OnClick()
  {
    if (transform.GetComponent<Button>().interactable)
    {
      GameInteface.Instance.transform.localPosition += _moveCoordinates;
    }
  }
}