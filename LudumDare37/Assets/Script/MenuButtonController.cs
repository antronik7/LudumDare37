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
        transform.Find("levelButtonText").GetComponent<Text>().text = level.ToString();
        if (Scorer.instance.getScoreValue(0) < level-1)
        {
            Debug.Log(level);
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
        valid();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + level);
    }

    public void valid()
    {
        AudioController.instance.playClip(15);
    }

    public void onSelect()
    {
        AudioController.instance.playClip(14);
        if(level < 6)
        {
            transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
        }
        if (level > 10)
        {
            transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0.6f;
        }
    }

}
