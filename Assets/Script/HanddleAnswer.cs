using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanddleAnswer : MonoBehaviour
{
    public void HanddleCorrect()
    {
        PlayerPrefs.SetString("Reset", "false");
        //PlayerPrefs.SetInt("Startgame", 1);
        PlayerPrefs.Save();
    }

    public void HanddleWrong()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("Reset", "true");
        PlayerPrefs.SetInt("Checkpoint", 0);

        PlayerPrefs.Save();
    }
}
