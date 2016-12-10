using UnityEngine;
using System.Collections;

public class HookController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player")
        {
            AttractPlayer(other);
            other.GetComponent<PlayerController>().hook = transform;
            other.GetComponent<PlayerController>().isHooked = true;
        }
    }

    void AttractPlayer(Collider2D player)
    {
        player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * 5);
    }
}
