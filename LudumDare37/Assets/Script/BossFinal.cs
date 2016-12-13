using UnityEngine;
using System.Collections;

public class BossFinal : MonoBehaviour {

	private GameObject player;
	public float moveSpeed;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		transform.position = Vector3.MoveTowards(player.transform.position, transform.position, Time.deltaTime * moveSpeed);
		Vector3 test = new Vector3 (transform.position.x, transform.position.y, 0f);
		transform.position = test;
		
	}
}
