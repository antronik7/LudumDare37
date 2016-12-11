﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pauseInterface : MonoBehaviour {

    public GameObject menuPanel;
    public void closeGame()
    {
        Application.Quit();
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void returnMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void swapMenuPanel()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    private static pauseInterface s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static pauseInterface instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(pauseInterface)) as pauseInterface;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                return null;
            }
            return s_Instance;
        }
    }
}