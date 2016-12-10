using UnityEngine;
using System.Collections;

public class KeyLockController : MonoBehaviour {

   	private GameObject lockKey;
	private GameObject exitDoor;

	void Start () {
		exitDoor = transform.parent.parent.gameObject;
		lockKey = this.transform.parent.gameObject;
		lockKey.transform.position = exitDoor.transform.position;
		exitDoor.GetComponent<ExitController> ().setIsKey (true);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			exitDoor.GetComponent<ExitController> ().setIsKey (false);
			lockKey.SetActive (false);
			this.gameObject.SetActive (false);
		}
    }
}
