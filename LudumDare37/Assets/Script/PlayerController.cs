using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject OneRoom;

    public float m_speed;
    public float j_force;
    public bool IsGround = false;
    public BoxCollider2D boxCollider;

    private Rigidbody2D rBody;
    

    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxisRaw("Horizontal");
        rBody.velocity = new Vector2(move * m_speed, rBody.velocity.y);

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
    }
}
