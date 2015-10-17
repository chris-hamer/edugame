using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    float ACCEL_RATE = 10.0f;
    float DECEL_RATE = 0.175f;
    float JUMP_POWER = 80.0f;
    float UNJUMP_RATE = 0.25f;
    float MAX_SLIDE_TIME = 0.5f;

    public Sprite standsprite;
    public Sprite slidesprite;

    public GameObject maincamera;

    Vector3 DEFAULT_SCALE = new Vector3(0.7f,1.4f,1.0f);
    Vector3 SLIDE_SCALE = new Vector3(0.7f, 0.7f, 1.0f);

    Vector3 Velocity;
    float slidetimer;
    bool lookslikeweregonnahavetojump;
    bool justslide;
    bool unjump;
    bool OnTheGround;
    bool slidelocationadjustlock;
    bool cantstopwontstop;

	// Use this for initialization
	void Start () {

	}

    void OnEnable()
    {
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void Jump()
    {
        if (!GetComponentInChildren<SlideCheck>().cant) {
            lookslikeweregonnahavetojump = true;
        }
    }

    public void UnJump()
    {
        unjump = true;
    }

    public void Slide()
    {
        justslide = true;
    }

    public void UnSlide()
    {
        if(!GetComponentInChildren<SlideCheck>().cant) {
            justslide = false;
        }
    }

    void Update()
    {
        //Touch t = Input.touches[0];
        if (Input.GetKeyDown(KeyCode.Space) || false) {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            Slide();
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            UnSlide();
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            UnJump();
        }
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {

        Velocity = GetComponent<Rigidbody2D>().velocity;

        Vector2 Acceleration = Vector2.zero;
        Acceleration += new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f) * ACCEL_RATE;
        Acceleration.x -= (Velocity.x - 50.0f) * DECEL_RATE;

        //Debug.DrawLine(transform.localPosition + Vector3.right * transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2.0f + -Vector3.up * transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2.0f, transform.localPosition - Vector3.right * transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2.0f + -Vector3.up * transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2.0f - Vector3.up * 10.0f);

        if (Physics2D.Raycast(transform.localPosition + (Vector3)GetComponent<BoxCollider2D>().offset * 8.0f - Vector3.right * transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2.0f + -Vector3.up * transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2.0f, -Vector2.up, 0.75f) ||
            Physics2D.Raycast(transform.localPosition + (Vector3)GetComponent<BoxCollider2D>().offset * 8.0f + Vector3.right * transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2.0f + -Vector3.up * transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2.0f, -Vector2.up, 0.75f)) {
            OnTheGround = true;
        }

        if (OnTheGround) {
            if (lookslikeweregonnahavetojump) {
                Acceleration.y += JUMP_POWER;
                OnTheGround = false;
                justslide = false;
            }
        }

        if (unjump && !OnTheGround && Velocity.y > 0.0f) {
            GetComponent<Rigidbody2D>().velocity += UNJUMP_RATE * Physics2D.gravity * (Velocity.y/JUMP_POWER);
        }

        GetComponent<Rigidbody2D>().AddForce(Acceleration, ForceMode2D.Impulse);

        if (slidetimer > MAX_SLIDE_TIME && !GetComponentInChildren<SlideCheck>().cant) {
            justslide = false;
        }

        if (justslide) {
            if (OnTheGround) {
                slidetimer += Time.fixedDeltaTime;
                GetComponents<BoxCollider2D>()[0].size = SLIDE_SCALE;
                if (!slidelocationadjustlock) {
                    GetComponents<BoxCollider2D>()[0].offset -= new Vector2(0.0f, SLIDE_SCALE.y / 2.0f);
                    slidelocationadjustlock = true;
                }
            } else {
                //justslide = false;
            }
        } else {
            slidetimer = 0.0f;
            GetComponents<BoxCollider2D>()[0].size = DEFAULT_SCALE;
            if (slidelocationadjustlock) {
                GetComponents<BoxCollider2D>()[0].offset += new Vector2(0.0f, SLIDE_SCALE.y / 2.0f);
                slidelocationadjustlock = false;
            }
        }

        GetComponent<Animator>().SetFloat("Velocity", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        GetComponent<Animator>().SetBool("Jump", !OnTheGround || lookslikeweregonnahavetojump || (GetComponent<Rigidbody2D>().velocity.y > 0.2f));
        GetComponent<Animator>().SetBool("Slide", justslide);

        OnTheGround = false;
        lookslikeweregonnahavetojump = false;
        unjump = false;
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inbounds") {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            maincamera.transform.position = new Vector3(0.0f,0.0f,-10.0f);
        }
    }

}
