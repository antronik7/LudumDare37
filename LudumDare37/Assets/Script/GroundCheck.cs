using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    PlayerController parent;

    // Use this for initialization
    void Start () {
        parent = transform.parent.gameObject.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            parent.GetComponent<PlayerController>().IsGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            parent.GetComponent<PlayerController>().IsGround = false;
        }
    }
}
