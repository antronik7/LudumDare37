using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private GameObject OneRoom;

    public float m_speed;
    public float j_force;
    public bool IsGround = false;
    GameObject LaCamera;
    public BoxCollider2D boxCollider;
    

    private Rigidbody2D rBody;
    private bool CanMove = true;

    public bool isHooked = false;
    public Transform hook = null;

	private float baseGravityScale;
    private int actionPlayer;
    private int directionRotation;

    public AudioClip jumpSound;
    public AudioClip translationSound;
    public AudioClip rotationSound;
    public AudioClip symetrieSound;

    public AudioClip rewindTranslationSound;
    public AudioClip rewindRotationSound;
    public AudioClip rewindSymetrieSound;

    void Start () {
        rBody = GetComponent<Rigidbody2D>();
		baseGravityScale = rBody.gravityScale;
        boxCollider = GetComponent<BoxCollider2D>();
		OneRoom = GameObject.FindGameObjectWithTag ("Room");
        LaCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

	void Update () {

        if (isHooked)
        {
            if(hook != null)
            {
                rBody.velocity = new Vector2(0, 0);
                transform.position = hook.position;
            }

            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<AudioSource>().PlayOneShot(jumpSound, 1f);
                rBody.velocity = new Vector2(rBody.velocity.x, j_force);

                IsGround = false;
                isHooked = false;
                hook = null;
            }
        }

        if (CanMove)
        {
            if(!isHooked)
            {
                float move = Input.GetAxisRaw("Horizontal");
                rBody.velocity = new Vector2(move * m_speed, rBody.velocity.y);
            }
            
            if (IsGround)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    GetComponent<AudioSource>().PlayOneShot(jumpSound, 1f);
                    rBody.velocity = new Vector2(rBody.velocity.x, j_force);

                    IsGround = false;
                }
            }

            if (Input.GetButtonDown("Translation"))
            {
                if(RessourceManager.instance.NbrTranslation > 0)
                {
                    actionPlayer = 1;
                    RessourceManager.instance.NbrTranslation--;

                    GetComponent<AudioSource>().PlayOneShot(translationSound, 1f);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;

                    LaCamera.GetComponent<CameraController>().faitDezoom = true;

                    //OneRoom.GetComponent<OneRoomController>().OneRoomTranslation(transform.position);
                }
            }

            if (Input.GetAxis("RotGauche") > 0.1)
            {
                if(RessourceManager.instance.NbrRotation > 0)
                {
                    actionPlayer = 2;

                    RessourceManager.instance.NbrRotation--;

                    GetComponent<AudioSource>().PlayOneShot(rotationSound, 1f);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;
                    rBody.constraints = RigidbodyConstraints2D.None;

                    StartCoroutine(WaitForRotation(1));
                }
            }

			if (Input.GetAxis("RotDroite") > 0.1)
            {
                if(RessourceManager.instance.NbrRotation > 0)
                {
                    actionPlayer = 2;

                    RessourceManager.instance.NbrRotation--;

                    GetComponent<AudioSource>().PlayOneShot(rotationSound, 1f);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;
                    rBody.constraints = RigidbodyConstraints2D.None;
                    StartCoroutine(WaitForRotation(-1));
                }   
            }

            if (Input.GetButtonDown("Symetrie"))
            {
                if(RessourceManager.instance.NbrSymetrie > 0)
                {
                    actionPlayer = 3;

                    RessourceManager.instance.NbrSymetrie--;

                    GetComponent<AudioSource>().PlayOneShot(symetrieSound, 1f);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;

                    transform.parent = OneRoom.transform;

                    LaCamera.GetComponent<CameraController>().faitDezoom = true;

                    //OneRoom.GetComponent<OneRoomController>().OneRoomSymetrie();
                }
            }

            if (Input.GetButtonDown("Rewind"))
            {
                Rewinder.rewind();
            }
            }
            if (Input.GetButton("UnzoomCamera"))
            {
                CameraController.instance.unzoomCamera();
            }
            else
            {
                CameraController.instance.resetCamera();
            }
    }


    public void moveTo(Vector3 playerPosition)
    {
        gameObject.transform.position = playerPosition;
    }

    public void resetPhysic()
    {
        boxCollider.enabled = true;
        rBody.gravityScale = baseGravityScale;
        rBody.velocity = new Vector2(0, 0);
        rBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        CanMove = true;

        if(actionPlayer != 1)
        {
            LaCamera.GetComponent<CameraController>().faitZoom = true;
        }
        else
        {
            LaCamera.GetComponent<CameraController>().faitZoomTrans = true;
        }
        
    }

    public bool canIMove()
    {
        return CanMove;
    }

    IEnumerator WaitForRotation(int direction)
    {
        yield return new WaitForSeconds(0.5f);

        directionRotation = direction;

        LaCamera.GetComponent<CameraController>().faitDezoom = true;

        //OneRoom.GetComponent<OneRoomController>().OneRoomRotation(direction);
    }

    private static PlayerController s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static PlayerController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(PlayerController)) as PlayerController;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                Debug.Log("error");
                GameObject obj = new GameObject("Error");
                s_Instance = obj.AddComponent(typeof(PlayerController)) as PlayerController;
            }

            return s_Instance;
        }
    }

    public int getActionPlayer()
    {
        return actionPlayer;
    }

    public int getDirection()
    {
        return directionRotation;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }
}
