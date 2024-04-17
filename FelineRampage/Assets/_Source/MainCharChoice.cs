using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainCharChoice: MonoBehaviour
{
    [FormerlySerializedAs("ID")] public int _id;

    public void SaveMainChar()
    {
        PlayerPrefs.SetInt("MainChar", _id);
        SceneManager.LoadScene("StartTitres");
    }

    public void SkipStartTitres()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SkipFinalTitres()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}