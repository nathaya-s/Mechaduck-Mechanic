using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startgame : MonoBehaviour
{
    public void setNewGame()
    {
        // PlayerPrefs.SetInt("Startgame", 1);
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetString("Reset", "true");
        PlayerPrefs.Save();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        //PlayerPrefs.DeleteAll();
    }
}

