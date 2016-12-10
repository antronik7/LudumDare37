using UnityEngine;
using System.Collections;

public class OneRoomController : MonoBehaviour {

    public float speed;
    public float speedRotation;
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

            if (transform.position == target)
            {
                print("allo");
                DoTranslation = false;

                player.GetComponent<PlayerController>().resetPhysic();
            }
        }

        if(DoRotation)
        {
            if(direction > 0)
            {
                Debug.Log(transform.rotation.eulerAngles.z);
                Debug.Log(anglePrecedent);

                if (transform.rotation.eulerAngles.z < anglePrecedent)
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

                anglePrecedent = transform.rotation.eulerAngles.z;
            }
            else
            {

                if (transform.rotation.eulerAngles.z > anglePrecedent )
                {
                    DoRotation = false;
                    GetComponent<Rigidbody2D>().angularVelocity = 0;

                    transform.rotation = Quaternion.Euler(0, 0, angleInitial - 90);

                    player.GetComponent<PlayerController>().resetPhysic();
                }
                else
                {
                    if (transform.rotation.eulerAngles.z < angleInitial - 90 && transform.rotation.eulerAngles.z != 0)
                    {
                        DoRotation = false;
                        GetComponent<Rigidbody2D>().angularVelocity = 0;

                        transform.rotation = Quaternion.Euler(0, 0, angleInitial - 90);

                        player.GetComponent<PlayerController>().resetPhysic();
                    }
                }

                anglePrecedent = transform.rotation.eulerAngles.z;

                if (anglePrecedent == 0)
                {
                    anglePrecedent = 360;
                }
            }

            
        }
	}

    public void OneRoomTranslation(Vector3 position)
    {
        target = position + offset;

        DoTranslation = true;
    }

    public void OneRoomRotation(int dir)
    {
        GetComponent<Rigidbody2D>().angularVelocity = speedRotation * dir;

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
