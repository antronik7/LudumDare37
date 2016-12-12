using UnityEngine;
using System.Collections;

public class OneRoomController : MonoBehaviour {

    private GameObject player;
    public float speed;
    public float speedRotation;
    public float speedSymetrie;
    public GameObject spawn;
    public GameObject rotationHook;

    Vector3 offset;
    bool DoTranslation = false;
    bool DoRotation = false;
    bool DoSymetrie = false;
    float angleInitial;
    float angleInitialPlayer;
    float anglePrecedent;
    int direction;
    float transitionInitialDistancePlayerSpawn = 0;
    float ScaleTarget;
    GameObject leTrucATourner;
    GameObject laCamera;

    float step;
    Vector3 target;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
        step = speed * Time.deltaTime;
        laCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //spawn = (FindObjectOfType(typeof(SpawnController)) as SpawnController).gameObject;
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


                laCamera.GetComponent<CameraController>().distanceEntreCameraEtRoom = Vector2.Distance(laCamera.transform.position, transform.position);

                player.GetComponent<PlayerController>().resetPhysic();

                
            }
        }

        if(DoSymetrie)
        {
            if(transform.rotation.eulerAngles.z == 270 || transform.rotation.eulerAngles.z == 90)
            {
                if (ScaleTarget < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - Time.deltaTime * speedSymetrie, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * speedSymetrie, transform.localScale.z);
                }

                if (Mathf.Abs(transform.localScale.y) > Mathf.Abs(ScaleTarget))
                {
                    DoSymetrie = false;
                    transform.localScale = new Vector3(transform.localScale.x, ScaleTarget, transform.localScale.z);
                    player.transform.parent = null;
                    player.GetComponent<PlayerController>().resetPhysic();
                }
            }
            else
            {
                if (ScaleTarget < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * speedSymetrie, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * speedSymetrie, transform.localScale.y, transform.localScale.z);
                }

                if (Mathf.Abs(transform.localScale.x) > Mathf.Abs(ScaleTarget))
                {
                    DoSymetrie = false;

                    transform.localScale = new Vector3(ScaleTarget, transform.localScale.y, transform.localScale.z);
                    player.transform.parent = null;
                    player.GetComponent<PlayerController>().resetPhysic();

                    
                }
            }
            
        }

        if(DoRotation)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, angleInitialPlayer);

            if(player.GetComponent<PlayerController>().isHooked == false)
            {
                player.transform.RotateAround(transform.position, Vector3.forward, direction * Time.deltaTime * speedRotation);
            }

            if (direction > 0)
            {
                if (leTrucATourner.transform.rotation.eulerAngles.z < anglePrecedent)
                {
                    finirRotation();
                }
                else
                {
                    if (leTrucATourner.transform.rotation.eulerAngles.z > angleInitial + 90)
                    {
                        finirRotation();
                    }
                }

                anglePrecedent = leTrucATourner.transform.rotation.eulerAngles.z;
            }
            else
            {
                if ((int)leTrucATourner.transform.rotation.eulerAngles.z > anglePrecedent)
                {
                    finirRotation();
                }
                else
                {
                    if ((int)leTrucATourner.transform.rotation.eulerAngles.z < angleInitial - 90 && (int)leTrucATourner.transform.rotation.eulerAngles.z != 0)
                    {
                        finirRotation();
                    }
                }

                anglePrecedent = (int)leTrucATourner.transform.rotation.eulerAngles.z;

                if (anglePrecedent == 0 && angleInitial != 90)
                {
                    anglePrecedent = 360;
                }
            }
        }
	}

    public void OneRoomTranslation(Vector3 position)
    {
        Rewinder.addTranslation(position, gameObject.transform.position, CameraController.instance.transform.position, PlayerController.instance.IsGround);

        offset = transform.position - spawn.transform.position;

        target = new Vector3(position.x, position.y, 0) + offset;

        DoTranslation = true;
    }

    public void OneRoomSymetrie()
    {
        Rewinder.addSymetrie(player.transform.position, CameraController.instance.transform.position, PlayerController.instance.IsGround);

        if (transform.rotation.eulerAngles.z == 270 || transform.rotation.eulerAngles.z == 90)
        {
            ScaleTarget = (int)transform.localScale.y * -1;
        }
        else
        {
            ScaleTarget = (int)transform.localScale.x * -1;
        }

        DoSymetrie = true;
    }

    public void OneRoomRotation(int dir)
    {
        Rewinder.addRotation(player.transform.position, dir, CameraController.instance.transform.position, PlayerController.instance.IsGround);

        if(player.GetComponent<PlayerController>().isHooked)
        {
            leTrucATourner = Instantiate(rotationHook, player.transform.position, Quaternion.identity) as GameObject;
            transform.parent = leTrucATourner.transform;
        }
        else
        {
            leTrucATourner = gameObject;
        }

        leTrucATourner.GetComponent<Rigidbody2D>().angularVelocity = speedRotation * dir;

        direction = dir;
        DoRotation = true;

        angleInitial = (int)leTrucATourner.transform.rotation.eulerAngles.z;
        angleInitialPlayer = (int)player.transform.rotation.eulerAngles.z;

        if (direction == -1 && (int)angleInitial == 0)
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

    public void moveTo(Vector3 roomPosition)
    {
        gameObject.transform.position = roomPosition;
    }

    public void doSymetrie()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void rotateTo(int dir)
    {
        if (dir > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z-90);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z+90);
        }
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

    void finirRotation()
    {
        DoRotation = false;

        leTrucATourner.GetComponent<Rigidbody2D>().angularVelocity = 0;

        Debug.Log(angleInitial);

        if(angleInitial == 179)
        {
            angleInitial = 180;
        }

        leTrucATourner.transform.rotation = Quaternion.Euler(0, 0, angleInitial + (90 * direction));

        player.transform.rotation = Quaternion.Euler(0, 0, angleInitialPlayer);

        player.GetComponent<PlayerController>().resetPhysic();

        player.transform.parent = null;

        if(player.GetComponent<PlayerController>().isHooked)
        {
            transform.parent = null;
            Destroy(leTrucATourner);
        }
    }
}
