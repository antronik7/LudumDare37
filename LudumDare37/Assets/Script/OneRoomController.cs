using UnityEngine;
using System.Collections;

public class OneRoomController : MonoBehaviour {

    public float speed;
    private GameObject player;
    public float speedRotation;
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
		player = GameObject.FindGameObjectWithTag ("Player");
        step = speed * Time.deltaTime;
        offset = transform.position - spawn.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if(DoTranslation)
        {
            step = speed * Time.deltaTime;

            transitionInitialDistancePlayerSpawn = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - spawn.transform.position.x), 2f) + Mathf.Pow((player.transform.position.y - spawn.transform.position.y), 2f));

            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (transform.position == target)
            {
                DoTranslation = false;

                player.GetComponent<PlayerController>().resetPhysic();
            }
        }

        if(DoRotation)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            player.transform.RotateAround(transform.position, Vector3.forward, direction * Time.deltaTime * speedRotation);

            if (direction > 0)
            {
                if (transform.rotation.eulerAngles.z < anglePrecedent)
                {
                    DoRotation = false;

                    GetComponent<Rigidbody2D>().angularVelocity = 0;

                    transform.rotation = Quaternion.Euler(0, 0, angleInitial + 90);
                    player.transform.rotation = Quaternion.Euler(0, 0, 0);

                    player.GetComponent<PlayerController>().resetPhysic();

                    player.transform.parent = null;
                }
                else
                {
                    if (transform.rotation.eulerAngles.z > angleInitial + 90)
                    {
                        DoRotation = false;

                        GetComponent<Rigidbody2D>().angularVelocity = 0;

                        transform.rotation = Quaternion.Euler(0, 0, angleInitial + 90);
                        player.transform.rotation = Quaternion.Euler(0, 0, 0);

                        player.GetComponent<PlayerController>().resetPhysic();

                        player.transform.parent = null;
                    }
                }

                anglePrecedent = transform.rotation.eulerAngles.z;
            }
            else
            {
                if ((int)transform.rotation.eulerAngles.z > anglePrecedent)
                {
                    DoRotation = false;

                    GetComponent<Rigidbody2D>().angularVelocity = 0;

                    transform.rotation = Quaternion.Euler(0, 0, angleInitial - 90);
                    player.transform.rotation = Quaternion.Euler(0, 0, 0);

                    player.GetComponent<PlayerController>().resetPhysic();

                    player.transform.parent = null;
                }
                else
                {
                    if ((int)transform.rotation.eulerAngles.z < angleInitial - 90 && (int)transform.rotation.eulerAngles.z != 0)
                    {
                        DoRotation = false;

                        GetComponent<Rigidbody2D>().angularVelocity = 0;

                        transform.rotation = Quaternion.Euler(0, 0, angleInitial - 90);
                        player.transform.rotation = Quaternion.Euler(0, 0, 0);

                        player.GetComponent<PlayerController>().resetPhysic();

                        player.transform.parent = null;
                    }
                }

                anglePrecedent = (int)transform.rotation.eulerAngles.z;

                if (anglePrecedent == 0 && angleInitial != 90)
                {
                    anglePrecedent = 360;
                }
            }
        }
	}

    public void OneRoomTranslation(Vector3 position)
    {
        Rewinder.addTranslation(position, gameObject.transform.position);
        target = position + offset;

        DoTranslation = true;
    }

    public void OneRoomRotation(int dir)
    {
        GetComponent<Rigidbody2D>().angularVelocity = speedRotation * dir;
        //player.GetComponent<Rigidbody2D>().angularVelocity = (speedRotation * 100) * dir * -1;

        //player.transform.SetParent(transform);

        direction = dir;
        DoRotation = true;

        angleInitial = (int)transform.rotation.eulerAngles.z;

        if (direction == -1 && (int)angleInitial == 0)
        {
            angleInitial = 360;
        }

        print(angleInitial);
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

    public void moveTo(Vector3 roomPosition)
    {
        gameObject.transform.position = roomPosition;
    }

    private static OneRoomController s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static OneRoomController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(OneRoomController)) as OneRoomController;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("OneRoom");
                s_Instance = obj.AddComponent(typeof(OneRoomController)) as OneRoomController;
            }

            return s_Instance;
        }
    }
}
