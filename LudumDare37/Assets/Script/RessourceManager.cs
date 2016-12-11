using UnityEngine;
using System.Collections;

public class RessourceManager : MonoBehaviour {

    public int NbrTranslation;
    public int NbrRotation;
    public int NbrSymetrie;

    public static RessourceManager instance = null;
 
    // Use this for initialization
    void Awake () {

        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
