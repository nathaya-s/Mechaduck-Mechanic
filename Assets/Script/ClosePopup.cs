using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClosePopupButton : MonoBehaviour
{
    public GameObject popupCanvas;
    private Button button;

    public Transform objectToReset;
    public Transform startingPosition;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClosePopup);
    }

    private void ClosePopup()
    {
        popupCanvas.SetActive(false);
        PlayerPrefs.DeleteAll();
        objectToReset.position = startingPosition.position;
        PlayerPrefs.SetString("Reset", "true");
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.Save();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}