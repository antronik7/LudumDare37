using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour {

    public string levelName;
	private bool isKey = false;
	public void setIsKey(bool val){isKey = val;}
	public bool getIsKey(){return isKey;}

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D other)
    {
        //print("TriggerEnter");
        //Si joueur :
		if (isKey)
			return;
        if(other.tag == "Player")
        {
            float levelNumber = float.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Substring(5, 1));
            Scorer.instance.addScoreValue(0, levelNumber);

            LevelController.instance.setLevelScore();
            AudioController.instance.playClip(13);
            StartCoroutine(LoadLevelCoroutine());
        }
    }

    IEnumerator LoadLevelCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    private static ExitController s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static ExitController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(ExitController)) as ExitController;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                Debug.Log("error");
                GameObject obj = new GameObject("Error");
                s_Instance = obj.AddComponent(typeof(ExitController)) as ExitController;
            }

            return s_Instance;
        }
    }
}
