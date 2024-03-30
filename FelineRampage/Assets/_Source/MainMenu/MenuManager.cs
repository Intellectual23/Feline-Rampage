using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject _settingsPanel;

        // Start is called before the first frame update
        void Start()
        {
            _settingsPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NewGame()
        {
            PlayerPrefs.SetInt("Load", 0);
            SceneManager.LoadScene("GameScene");
        }

        public void Continue()
        {
            PlayerPrefs.SetInt("Load", 1);
            SceneManager.LoadScene("GameScene");
        }

        public void Settings()
        {
            _settingsPanel.SetActive(true);
        }

        public void Exit()
        {
            Debug.Log("dfdkjfkdjfkdf");
            Application.Quit();
        }
    }
}
