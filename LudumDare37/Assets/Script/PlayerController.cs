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

	private Animator animManager;

	private bool isSymetrie=false;

	public bool test;

    void Start () {
		animManager = GetComponent<Animator> ();
        rBody = GetComponent<Rigidbody2D>();
		baseGravityScale = rBody.gravityScale;
        boxCollider = GetComponent<BoxCollider2D>();
		OneRoom = GameObject.FindGameObjectWithTag ("Room");
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
                AudioController.instance.playClip(0);
                rBody.velocity = new Vector2(rBody.velocity.x, j_force);
				animManager.SetBool ("isJumping", true);

                IsGround = false;
                isHooked = false;
                hook = null;
            }
        }
		if ((rBody.velocity.y < 0)&&(!IsGround)) {
			animManager.SetBool ("isFalling", true);
		} else if (IsGround) {
			animManager.SetBool ("isFalling", false);
			animManager.SetBool ("isJumping", false);
		}

        if (CanMove)
        {
            if(!isHooked)
            {
                float move = Input.GetAxisRaw("Horizontal");
				if (move == 0) {
					animManager.SetBool ("isWalking", false);
				} else {
					if (move < 0) {
						if(!isSymetrie)
							GetComponent<SpriteRenderer> ().flipX = true;
						else
							GetComponent<SpriteRenderer> ().flipX = false;
					} else {
						if(!isSymetrie)
							GetComponent<SpriteRenderer> ().flipX = false;
						else
							GetComponent<SpriteRenderer> ().flipX = true;
					}
					animManager.SetBool ("isWalking", true);
				}
                rBody.velocity = new Vector2(move * m_speed, rBody.velocity.y);
            }
            
            if (IsGround)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    AudioController.instance.playClip(0);
                    rBody.velocity = new Vector2(rBody.velocity.x, j_force);
					animManager.SetBool ("isJumping", true);
                    IsGround = false;
                }
            }

            if (Input.GetButtonDown("Translation"))
            {
                if(RessourceManager.instance.NbrTranslation > 0)
                {
                    RessourceManager.instance.NbrTranslation--;

                    AudioController.instance.playClip(2);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;

                    OneRoom.GetComponent<OneRoomController>().OneRoomTranslation(transform.position);
                }
            }

            if (Input.GetAxis("RotGauche") > 0.1)
            {
                if(RessourceManager.instance.NbrRotation > 0)
                {
                    RessourceManager.instance.NbrRotation--;

                    AudioController.instance.playClip(4);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;
                    rBody.constraints = RigidbodyConstraints2D.None;

                    StartCoroutine(WaitForRotation(1));
                }
            }

			if ((Input.GetAxis("RotDroite") > 0.1)||(test))
            {
                if(RessourceManager.instance.NbrRotation > 0)
                {
                    RessourceManager.instance.NbrRotation--;

                    AudioController.instance.playClip(4);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;
                    rBody.constraints = RigidbodyConstraints2D.None;
                    StartCoroutine(WaitForRotation(-1));
                }   
				test=false;
            }

            if (Input.GetButtonDown("Symetrie"))
            {
                if(RessourceManager.instance.NbrSymetrie > 0)
                {
                    RessourceManager.instance.NbrSymetrie--;

                    AudioController.instance.playClip(6);

                    boxCollider.enabled = false;
                    rBody.gravityScale = 0;
                    rBody.velocity = new Vector2(0, 0);
                    CanMove = false;

					isSymetrie = !isSymetrie;
					GetComponent<SpriteRenderer> ().flipX = !GetComponent<SpriteRenderer> ().flipX;

                    transform.parent = OneRoom.transform;

                    OneRoom.GetComponent<OneRoomController>().OneRoomSymetrie();
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

            if (Input.GetButtonDown("Pause"))
            {
                pauseInterface.instance.swapMenuPanel();
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

    public bool canIMove()
    {
        return CanMove;
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
