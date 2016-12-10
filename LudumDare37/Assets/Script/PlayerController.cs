using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject OneRoom;

    public float m_speed;
    public float j_force;
    public bool IsGround = false;
    public BoxCollider2D boxCollider;

    private Rigidbody2D rBody;
    private bool CanMove = true;

    public bool isHooked = false;
    public Transform hook = null;

    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
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
            OneRoom.GetComponent<OneRoomController>().OneRoomTranslation(transform.position);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            OneRoom.GetComponent<OneRoomController>().OneRoomRotation(1);

            boxCollider.enabled = false;
            rBody.gravityScale = 0;
            rBody.velocity = new Vector2(0, 0);
            CanMove = false;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            OneRoom.GetComponent<OneRoomController>().OneRoomRotation(-1);

            rBody.gravityScale = 0;
            rBody.velocity = new Vector2(0, 0);
            CanMove = false;
        }
    }

    public void resetPhysic()
    {
        rBody.gravityScale = 10;
        rBody.velocity = new Vector2(0, 0);
        CanMove = true;
    }
}
