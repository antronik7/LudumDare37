using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
[RequireComponent(typeof(Selectable))]
public class MenuButtonController : MonoBehaviour
{
    public float level;



	// Use this for initialization
	void Awake () {
        transform.Find("levelButtonText").GetComponent<Text>().text = "Level " + level;
        if (Scorer.instance.getScoreValue(0) < level-1)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            float scoreValue = Scorer.instance.getScoreValue("Level" + level);
            if (scoreValue>0)
            {
                transform.Find("FirstStar").GetComponent<Image>().color = Color.yellow;
            }
            if (scoreValue == 2 || scoreValue == 4)
            {
                transform.Find("SecondStar").GetComponent<Image>().color = Color.yellow;
            }
            if (scoreValue >= 3 )
            {
                transform.Find("ThirdStar").GetComponent<Image>().color = Color.yellow;
            }
        }
    }
    public void loadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + level);
    }

}
