﻿using UnityEngine;
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
            GetComponent<Button>().interactable = false;
        }
        else
        {
            Color yellow;
            ColorUtility.TryParseHtmlString("#C9D311FF", out yellow);
            float scoreValue = Scorer.instance.getScoreValue("Level" + level);
            if (scoreValue >= 2)
            {
                transform.Find("SecondStar").GetComponent<Image>().color = yellow;
            }

            /*
            if (scoreValue>0)
            {
                transform.Find("FirstStar").GetComponent<Image>().color = yellow;
            }
            if (scoreValue == 2 || scoreValue == 4)
            {
                transform.Find("SecondStar").GetComponent<Image>().color = yellow;
            }
            if (scoreValue >= 3 )
            {
                transform.Find("ThirdStar").GetComponent<Image>().color = yellow;
            }*/
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
        if (level > 10 && level < 16)
        {
            transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0.75f;
        }
        if (level > 15 && level < 21)
        {
            transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0.45f;
        }
        if (level > 20 && level < 26)
        {
            transform.parent.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0.025f;
        }
    }

}
