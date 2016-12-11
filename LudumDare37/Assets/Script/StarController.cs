using UnityEngine;
using System.Collections;

public class StarController : MonoBehaviour {
    private GameObject lockKey;
    private GameObject exitDoor;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Rewinder.gotStar();
            this.gameObject.SetActive(false);
        }
    }

    public void resetStar()
    {
        this.gameObject.SetActive(true);
    }


    private static StarController s_Instance = null;

    public static StarController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(StarController)) as StarController;
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
