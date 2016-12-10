using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject OneRoom;
    private OneRoomController roomController;

    private float normalSizeRoom;
    public float rotateSizeRoom;

    void Start()
    {
        roomController = OneRoom.GetComponent<OneRoomController>();
        normalSizeRoom = GetComponent<Camera>().orthographicSize;
    }

    void Update()
    {
        transform.position = new Vector3(OneRoom.transform.position.x, OneRoom.transform.position.y, transform.position.z);
        if (roomController.getIsTranslating())
        {
            if (roomController.getDistanceBetweenPlayerSpawner() > (roomController.getInitialDistanceBetweenPlayerSpawn() / 2))
                GetComponent<Camera>().orthographicSize += 0.02f;
            else
            {
                GetComponent<Camera>().orthographicSize -= 0.02f;
            }

        }
        else if (GetComponent<Camera>().orthographicSize > normalSizeRoom)
        {
            GetComponent<Camera>().orthographicSize = normalSizeRoom;
        }
    }
}
