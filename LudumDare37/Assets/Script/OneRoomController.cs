using UnityEngine;
using System.Collections;

public class OneRoomController : MonoBehaviour {

    public float speed;
    public GameObject player;
    public GameObject spawn;

    Vector3 offset;
    bool DoTranslation = false;
    float step;
    Vector3 target;

    // Use this for initialization
    void Start () {
        step = speed * Time.deltaTime;
        offset = transform.position - spawn.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(DoTranslation)
        {
            step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target, step);

            print(transform.position);
            print(target);

            if (transform.position == target)
            {
                DoTranslation = false;

                Time.timeScale = 1.0F;
                player.GetComponent<PlayerController>().boxCollider.enabled = true;
                Debug.Log("Fini translation");
            }
        }
	}

    public void OneRoomTranslation(Vector3 position)
    {
        target = position + offset;

        Time.timeScale = 0.1F;
        DoTranslation = true;
    }
}
