using UnityEngine;
using UnityEngine.UI;

public class ContinuePopup : MonoBehaviour
{
    private Button button;
    public GameObject popupCanvas;
    public GameObject[] QuestionCanvas;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(QuestionPopup);
    }

    private void QuestionPopup()
    {
        int RandomIndex = Random.Range(0, QuestionCanvas.Length);
        GameObject RandomPopup = QuestionCanvas[RandomIndex];
        RandomPopup.SetActive(true);
        popupCanvas.SetActive(false);
    }
}
