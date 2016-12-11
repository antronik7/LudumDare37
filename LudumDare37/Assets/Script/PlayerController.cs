using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private GameObject OneRoom;

    public float m_speed;
    public float j_force;
    public bool IsGround = false;
    public BoxCollider2D boxCollider;

    private Rigidbody2D rBody;
    private bool CanMove = true;

    public bool isHooked = false;
    public Transform hook = null;

	private float baseGravityScale;

    void Start () {
        rBody = GetComponent<Rigidbody2D>();
		baseGravityScale = rBody.gravityScale;
        boxCollider = GetComponent<BoxCollider2D>();
		OneRoom = GameObject.FindGameObjectWithTag ("Room");
    }

	void Update () {

        if (isHooked)
        {
            print("isHooked");
            if(hook != null)
            {
                rBody.velocity = new Vector2(0, 0);
                transform.position = hook.position;
            }

            if (Input.GetButtonDown("Jump"))
            {
                rBody.velocity = new Vector2(rBody.velocity.x, j_force);

                IsGround = false;
                isHooked = false;
                hook = null;
            }
        }
        else if (CanMove)
        {
            float move = Input.GetAxisRaw("Horizontal");
            rBody.velocity = new Vector2(move * m_speed, rBody.velocity.y);
        }

        if (IsGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rBody.velocity = new Vector2(rBody.velocity.x, j_force);

                IsGround = false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            boxCollider.enabled = false;
            rBody.gravityScale = 0;
            rBody.velocity = new Vector2(0, 0);
            CanMove = false;
            
            OneRoom.GetComponent<OneRoomController>().OneRoomTranslation(transform.position);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            boxCollider.enabled = false;
            rBody.gravityScale = 0;
            rBody.velocity = new Vector2(0, 0);
            CanMove = false;
            rBody.constraints = RigidbodyConstraints2D.None;

            StartCoroutine(WaitForRotation(1));
        }

        if (Input.GetButtonDown("Fire3"))
        {
            boxCollider.enabled = false;
            rBody.gravityScale = 0;
            rBody.velocity = new Vector2(0, 0);
            CanMove = false;
            rBody.constraints = RigidbodyConstraints2D.None;

            StartCoroutine(WaitForRotation(-1));
        }

        if (Input.GetButtonDown("Rewind"))
        {
            Rewinder.rewind();
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
    }

    IEnumerator WaitForRotation(int direction)
    {
        yield return new WaitForSeconds(0.5f);

        OneRoom.GetComponent<OneRoomController>().OneRoomRotation(direction);
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
}
