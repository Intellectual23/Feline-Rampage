using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Interface
{
  public class InterfaceLog : MonoBehaviour
  {
    public int _linesCount;
    public static InterfaceLog Instance;
    private List<string> _messages = new();
    public List<TextMeshProUGUI> _textLines;
    private int MessageCount { get; set; } = 0;

    private void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
      }
      else
      {
        Destroy(gameObject);
      }
      
    }

    private void Start()
    {
      for (int i = 0; i < _linesCount; ++i)
      {
        _messages.Add("");
        _textLines[i].text = "";
      }
    }
    
    public void AddMessage(string message)
    {
      _messages[MessageCount % _linesCount] = message;
      ++MessageCount;
      UpdateLog();
    }

    private void UpdateLog()
    {
      for (int i = 0; i < _linesCount; ++i)
      {
        _textLines[i].text = _messages[i];
      }
    }
  }
}
