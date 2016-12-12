using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject startGameButton;
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
    }



    public void closeGame()
    {
        Application.Quit();
    }
    public void changeController(float value)
    {
        Scorer.instance.setScore("controllerChoice", value);
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
