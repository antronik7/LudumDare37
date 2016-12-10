using UnityEngine;
using System.Collections;

public class OneRoomController : MonoBehaviour {

    public float speed;
    public GameObject player;
    public GameObject spawn;

    Vector3 offset;
    bool DoTranslation = false;
    bool DoRotation = false;
    float angleInitial;
    float anglePrecedent;
    int direction;
    float transitionInitialDistancePlayerSpawn = 0;

    float step;
    Vector3 target;

    // Use this for initialization
    void Start () {
        step = speed * Time.deltaTime;
        offset = transform.position - spawn.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(transform.rotation.eulerAngles);

        if(DoTranslation)
        {
            step = speed * Time.deltaTime;

            transitionInitialDistancePlayerSpawn = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - spawn.transform.position.x), 2f) + Mathf.Pow((player.transform.position.y - spawn.transform.position.y), 2f));

            transform.position = Vector3.MoveTowards(transform.position, target, step);

            print(transform.position);
            print(target);

            if (transform.position == target)
            {
                DoTranslation = false;

                Time.timeScale = 1.0F;
                Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;

                player.GetComponent<PlayerController>().boxCollider.enabled = true;
                Debug.Log("Fini translation");
            }
        }

        if(DoRotation)
        {
            if(direction > 0)
            {
                if(transform.rotation.eulerAngles.z < anglePrecedent)
                {
                    DoRotation = false;

                    GetComponent<Rigidbody2D>().angularVelocity = 0;

                    transform.rotation = Quaternion.Euler(0, 0, angleInitial + 90);

                    player.GetComponent<PlayerController>().resetPhysic();
                }
                else
                {
                    if (transform.rotation.eulerAngles.z > angleInitial + 90)
                    {
                        DoRotation = false;

                        GetComponent<Rigidbody2D>().angularVelocity = 0;

                        transform.rotation = Quaternion.Euler(0, 0, angleInitial + 90);

                        player.GetComponent<PlayerController>().resetPhysic();
                    }
                }
            }
            else
            {
                if (transform.rotation.eulerAngles.z > anglePrecedent)
                {
                    DoRotation = false;
                    GetComponent<Rigidbody2D>().angularVelocity = 0;

                    transform.rotation = Quaternion.Euler(0, 0, angleInitial - 90);

                    player.GetComponent<PlayerController>().resetPhysic();
                }
                else
                {
                    if (transform.rotation.eulerAngles.z < angleInitial - 90)
                    {
                        DoRotation = false;
                        GetComponent<Rigidbody2D>().angularVelocity = 0;

                        transform.rotation = Quaternion.Euler(0, 0, angleInitial - 90);

                        player.GetComponent<PlayerController>().resetPhysic();
                    }
                }
            }

            anglePrecedent = transform.rotation.eulerAngles.z;
        }
	}

    public void OneRoomTranslation(Vector3 position)
    {
        target = position + offset;

        Time.timeScale = 0.2F;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        DoTranslation = true;
    }

    public void OneRoomRotation(int dir)
    {
        GetComponent<Rigidbody2D>().angularVelocity = 300 * dir;

        direction = dir;
        DoRotation = true;

        angleInitial = transform.rotation.eulerAngles.z;

        if (direction == -1 && angleInitial == 0)
        {
            angleInitial = 360;
        }

        anglePrecedent = angleInitial;
    }

    public bool getIsTranslating() { return this.DoTranslation; }

    public float getDistanceBetweenPlayerSpawner()
    {
        return Mathf.Sqrt(Mathf.Pow((player.transform.position.x - spawn.transform.position.x), 2f) + Mathf.Pow((player.transform.position.y - spawn.transform.position.y), 2f));
    }

    public float getInitialDistanceBetweenPlayerSpawn()
    {
        return transitionInitialDistancePlayerSpawn;
    }
}
