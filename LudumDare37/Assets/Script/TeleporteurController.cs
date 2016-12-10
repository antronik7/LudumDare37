using UnityEngine;
using System.Collections;

public class TeleporteurController : MonoBehaviour {

    public GameObject Player;
    public GameObject SortieWrap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.transform.position = SortieWrap.transform.position;
        }
    }
}
