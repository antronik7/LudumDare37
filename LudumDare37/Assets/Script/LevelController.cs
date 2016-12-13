using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public float minimumNecessaryAction;

	// Use this for initialization

    public void setLevelScore()
    {
        float levelScore = 1;
        if (StarController.instance == null || !StarController.instance.isActiveAndEnabled)
        {
            levelScore++;
        }
        /*
        if (Scorer.instance.getScoreValue("actionsNumber") <= minimumNecessaryAction)
        {
            levelScore += 2;
        }*/
        Scorer.instance.addScoreValue(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, levelScore);

        /*
        float bestLevelScore = Scorer.instance.getScoreValue(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        if (levelScore> bestLevelScore)
        {
            Scorer.instance.addScoreValue(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, levelScore);
        }*/
    }

    private static LevelController s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static LevelController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(LevelController)) as LevelController;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                
                Debug.Log("error");
                return null;
                GameObject obj = new GameObject("Error");
                s_Instance = obj.AddComponent(typeof(LevelController)) as LevelController;
            }

            return s_Instance;
        }
    }
}
