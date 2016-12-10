using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject OneRoom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(OneRoom.transform.position.x, OneRoom.transform.position.y, transform.position.z);
	}
}
