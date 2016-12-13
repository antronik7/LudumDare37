using UnityEngine;
using System.Collections;

public class FinalLevelColorEffect : MonoBehaviour {

	private Animator[] animLights;
	private float chrono;


	void Start () {
		animLights = GetComponentsInChildren<Animator> ();
		chrono = 1.5f;
	
	}

	void Update () {
		if (chrono > 0) {
			chrono -= Time.deltaTime;
		} else {
			launchAnimLight ();
		}
	}
		
	public void launchAnimLight(){
		int random = (int)Random.Range (0f, animLights.Length);
		animLights [random].SetTrigger ("glowing");
		chrono = 1.5f;
	}
}
