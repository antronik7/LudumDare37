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
            StartCoroutine(LoadLevelCoroutine());
        }
    }

    IEnumerator LoadLevelCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
