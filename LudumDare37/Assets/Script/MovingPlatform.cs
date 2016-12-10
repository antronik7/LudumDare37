using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	float step;
	public float speed;

	public float distance;
	public bool isHorizontal;

	Vector3 target;

	public float timePaused;
	float timeChrono;
	bool isPaused = false;

	void Start () {
		timeChrono = timePaused;
		launchPlatform ();
	}

	void Update () {
		if (isPaused) {
			if (timeChrono <= 0) {
				timeChrono = timePaused;
				distance *= -1;
				isPaused = false;
				launchPlatform ();
			} else {
				timeChrono -= Time.deltaTime;
			}
		} else {
			step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target, step);
			if (transform.position == target) {
				isPaused = true;
			}
		}
	
	}

	public void launchPlatform(){
		if (isHorizontal) {
			target = new Vector3 (transform.position.x + distance, transform.position.y, transform.position.z);
		} else
			target = new Vector3 (transform.position.x, transform.position.y + distance, transform.position.z);
	}
}
