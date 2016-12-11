using UnityEngine;
using System.Collections;

public class KeyLockController : MonoBehaviour {

   	private GameObject lockKey;
	private GameObject exitDoor;
    public AudioClip unlockSound;


    void Start () {
		exitDoor = transform.parent.parent.gameObject;
		lockKey = this.transform.parent.gameObject;
		lockKey.transform.position = exitDoor.transform.position;
		exitDoor.GetComponent<ExitController> ().setIsKey (true);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(unlockSound, 1f);
            Rewinder.gotKey();
			exitDoor.GetComponent<ExitController> ().setIsKey (false);
			lockKey.SetActive (false);
			this.gameObject.SetActive (false);
		}
    }

    public void resetKey()
    {
        exitDoor.GetComponent<ExitController>().setIsKey(true);
        lockKey.SetActive(true);
        this.gameObject.SetActive(true);
    }


    private static KeyLockController s_Instance = null;

    public static KeyLockController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(KeyLockController)) as KeyLockController;
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
