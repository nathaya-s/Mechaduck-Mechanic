using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button[] answerButtons;
    public GameObject quizPopup;
    public int questionIndex;
    public int Select = -1;

    //waypoints
    //public Transform[] waypoints;
    //private Transform waypointNearest;

    //new game
    //private bool ActiveWaypoint = false; // if true -> get nearest waypoint, false -> new game

    //reset game
    //public Transform objectToReset;
    //public Transform startingPosition;

    private int[] Answer = { 1, 0, 0 };
    private void Start()
    {
        answerButtons = GameObject.FindObjectsOfType<Button>();
        foreach (Button answerButton in answerButtons)
        {
            answerButton.onClick.AddListener(() => AnswerButtonClicked());
        }
    }

    public void AnswerButtonClicked()
    {
        // quizPopup.SetActive(false);

        print("Question Index = " + questionIndex + "True Answer = " + Answer[questionIndex] + " Your Answer = " + Select);
        quizPopup.SetActive(false);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    //reset the game when the user selects the wrong answer
    private void ClosePopup()
    {
        print("------Wrong Answer---------");
        PlayerPrefs.DeleteAll();
        //objectToReset.position = startingPosition.position;
        PlayerPrefs.SetInt("Reset", -1);
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.Save();
        quizPopup.SetActive(false);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void HanddleCorrect()
    {
        quizPopup.SetActive(false);
        
        PlayerPrefs.SetString("Reset", "false");
        PlayerPrefs.Save();
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void HanddleWrong()
    {
        PlayerPrefs.SetString("Reset", "true");
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("Startgame", 1);
        PlayerPrefs.Save();
        quizPopup.SetActive(false);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
