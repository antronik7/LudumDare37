using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject startGameButton;
    public GameObject playKeyboardButton;
    public GameObject playControllerButton;
    float actualLevel;

    void Start()
    {
        if (AudioController.instance)
        {

        }

        actualLevel = Scorer.instance.getScoreValue(0);
        if(actualLevel > 14)//CHANGER ICI MAX LEVEL
        {
            actualLevel = 14;
        }

        if (actualLevel != 0)
        {
            startGameButton.GetComponentInChildren<Text>().text = "Continue";
        }
        selectColor();
    }



    public void closeGame()
    {
        Application.Quit();
    }
    public void changeController(float value)
    {
        Scorer.instance.setScore("controllerChoice", value);
        selectColor();
    }

    private void selectColor()
    {
        if (Scorer.instance.getScoreValue("controllerChoice") == 0)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#4E4A4AFF", out color);
            playKeyboardButton.GetComponent<Image>().color = color;

            ColorUtility.TryParseHtmlString("#FFFFFFFF", out color);
            playControllerButton.GetComponent<Image>().color = color;
        }
        else
        {
            Color color;
            ColorUtility.TryParseHtmlString("#FFFFFFFF", out color);
            playKeyboardButton.GetComponent<Image>().color = color;

            ColorUtility.TryParseHtmlString("#4E4A4AFF", out color);
            playControllerButton.GetComponent<Image>().color = color;
        }
    }

    public void goToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void startGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + (actualLevel + 1));
    }
}
