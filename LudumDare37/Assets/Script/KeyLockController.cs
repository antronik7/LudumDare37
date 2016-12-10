using UnityEngine;
using System.Collections;

public class KeyLockController : MonoBehaviour {

   public GameObject Key;
   public GameObject Lock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D() {
        Lock.SetActive(false);
        Key.SetActive(false);
    }
}
