using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject OneRoom;
    GameObject player;

    private OneRoomController roomController;


    private float normalSizeRoom;
    public float rotateSizeRoom;

    public float distanceDezoomAction;
    public float speedDeZoomAction;
    public float speedZoomAction;
    public float speedZoomTransition;
    public bool faitZoom = false;
    public bool faitDezoom = false;
    public bool faitZoomTrans = false;
    PlayerController playerController;

    bool entrainDeZoomer = false;


    private int init;
    private float initDistance;




    //variable pour translation
    Vector3 positionDebutTransition;
    public float distanceEntreCameraEtRoom;

    void Start()
    {
        roomController = OneRoom.GetComponent<OneRoomController>();
        normalSizeRoom = GetComponent<Camera>().orthographicSize;

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(faitZoom)
        {
            

            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - speedZoomAction * Time.deltaTime;

            
            if (GetComponent<Camera>().orthographicSize < normalSizeRoom)
            {
                faitZoom = false;

                GetComponent<Camera>().orthographicSize = normalSizeRoom;

                entrainDeZoomer = false;
            }
        }
        else if (faitDezoom)
        {
            entrainDeZoomer = true;

            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + speedDeZoomAction * Time.deltaTime;
            

            if (GetComponent<Camera>().orthographicSize > normalSizeRoom + distanceDezoomAction)
            {
                
                faitDezoom = false;

                GetComponent<Camera>().orthographicSize = normalSizeRoom + distanceDezoomAction;

                if(playerController.getActionPlayer() == 1)
                {
                    roomController.OneRoomTranslation(playerController.getPosition());

                }
                else if (playerController.getActionPlayer() == 2)
                {
                    roomController.OneRoomRotation(playerController.getDirection());
                }
                else
                {
                    roomController.OneRoomSymetrie();
                }
            }
        }
        else if(faitZoomTrans)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (OneRoom.transform.position.x, OneRoom.transform.position.y, -10f), speedZoomTransition * Time.deltaTime);

            float pourcentage = (distanceEntreCameraEtRoom - Vector2.Distance(transform.position, OneRoom.transform.position)) / distanceEntreCameraEtRoom;

            Debug.Log(pourcentage);

            GetComponent<Camera>().orthographicSize = ((normalSizeRoom + distanceDezoomAction) - ((distanceDezoomAction) * pourcentage));

            if(transform.position == new Vector3(OneRoom.transform.position.x, OneRoom.transform.position.y, -10f))
            {
                faitZoomTrans = false;

                GetComponent<Camera>().orthographicSize = normalSizeRoom;

                entrainDeZoomer = false;
            }
        }

        //transform.position = new Vector3(OneRoom.transform.position.x, OneRoom.transform.position.y, transform.position.z);

        /*if (roomController.getIsTranslating())
        {

            if (init < 2)
            {
                initDistance = roomController.getInitialDistanceBetweenPlayerSpawn();
                init++;
            }

            if (roomController.getDistanceBetweenPlayerSpawner() > (roomController.getInitialDistanceBetweenPlayerSpawn() / 2))
                GetComponent<Camera>().orthographicSize += 0.02f;
            else
            {
                GetComponent<Camera>().orthographicSize -= 0.02f;
            }

        }*/
        /*
        else if (GetComponent<Camera>().orthographicSize > normalSizeRoom)
        {
            GetComponent<Camera>().orthographicSize = normalSizeRoom;
        }*/
    }
    bool isMaxSize = false;

    public void unzoomCamera()
    {
        if (!isMaxSize)
        {
            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + 5 * Time.deltaTime;
            Vector3 screenPoint = GetComponent<Camera>().WorldToViewportPoint(ExitController.instance.transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0.1 && screenPoint.x < 0.9 && screenPoint.y > 0.1 && screenPoint.y < 0.9;
            if (onScreen)
            {
                if (StarController.instance == null)
                {
                    onScreen = true;
                }
                else
                {
                    screenPoint = GetComponent<Camera>().WorldToViewportPoint(StarController.instance.transform.position);
                    onScreen = screenPoint.z > 0 && screenPoint.x > 0.1 && screenPoint.x < 0.9 && screenPoint.y > 0.1 && screenPoint.y < 0.9;
                }
                if (onScreen)
                {
                    if (StarController.instance == null)
                    {
                        onScreen = true;
                    }
                    else
                    {
                        screenPoint = GetComponent<Camera>().WorldToViewportPoint(StarController.instance.transform.position);
                        onScreen = screenPoint.z > 0 && screenPoint.x > 0.1 && screenPoint.x < 0.9 && screenPoint.y > 0.1 && screenPoint.y < 0.9;
                    }
                    if (onScreen)
                    {
                        isMaxSize = true;
                    }
                }
            }
        }
    }

    public void resetCamera()
    {
        if(entrainDeZoomer == false)
        {
            if (gameObject.GetComponent<Camera>().orthographicSize > normalSizeRoom)
            {
                GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - 5 * Time.deltaTime;
            }
            isMaxSize = false;
        }
    }

    private static CameraController s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static CameraController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(CameraController)) as CameraController;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                Debug.Log("error");
                GameObject obj = new GameObject("Error");
                s_Instance = obj.AddComponent(typeof(CameraController)) as CameraController;
            }

            return s_Instance;
        }
    }

    public void zoomTrans()
    {

    }
}
