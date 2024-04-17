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
            //MusicSounds.Instance._musicManager.clip = MusicSounds.Instance._sounds[0];
            //MusicSounds.Instance._musicManager.Play();
            //Debug.Log("music play menumanager");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NewGame()
        {
            PlayerPrefs.SetInt("Load", 0);
            SceneManager.LoadScene("ChooseMainChar");
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

        public void ToMainMenu()
        {
            Serializer s = new Serializer();
            s.SaveGameData();
            SceneManager.LoadScene("MainMenuScene");
        }

        public void ContinueGame()
        {
            gameObject.SetActive(false);
        }

        private void DisableAllObjects(string sceneName)
        {
            Scene sceneToDisable = SceneManager.GetSceneByName(sceneName);
            if (sceneToDisable.IsValid())
            {
                foreach (GameObject obj in sceneToDisable.GetRootGameObjects())
                {
                    obj.SetActive(false);
                }
                Debug.Log("Все объекты на сцене " + sceneName + " были отключены.");
            }
        }

        private void EnableAllObjects(string sceneName)
        {
            Scene sceneToEnable = SceneManager.GetSceneByName(sceneName);
            if (!sceneToEnable.IsValid())
            {
                foreach (GameObject obj in sceneToEnable.GetRootGameObjects())
                {
                    obj.SetActive(true);
                }
                Debug.Log("Все объекты на сцене " + sceneName + " были включены.");
            }
        }
    }
}
