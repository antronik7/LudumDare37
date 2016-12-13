using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossFinal : MonoBehaviour {

	public GameObject player;
	public float moveSpeed;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		Vector3 newPosition = new Vector3 (player.transform.position.x - 22f, player.transform.position.y, 0F);
		transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * moveSpeed);
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		}
	}
}
