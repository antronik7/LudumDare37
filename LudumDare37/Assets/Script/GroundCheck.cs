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
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "movingPlatform")
        {
            if (!parent.IsGround)
            {
                //AudioController.instance.playClip(1);fout la merde
            }
            parent.IsGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "movingPlatform")
        {
            /*
            Debug.Log(2);*/
          //  AudioController.instance.playClip(1);
            parent.IsGround = true;

            if(parent.canIMove())
            {
                parent.transform.parent = other.transform;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            parent.IsGround = false;
        }
        else if (other.gameObject.tag == "movingPlatform")
        {
            parent.IsGround = false;

            if (parent.canIMove())
            {
                parent.transform.parent = null;
            }
        }
    }
}
