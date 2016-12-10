using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour {

    public string levelName;

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
