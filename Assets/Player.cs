using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    float ACCEL_RATE = 10.0f;
    float DECEL_RATE = 0.175f;
    float JUMP_POWER = 80.0f;
    float UNJUMP_RATE = 0.25f;
    float MAX_SLIDE_TIME = 0.5f;

    Vector3 DEFAULT_SCALE = new Vector3(4.0f,8.0f,4.0f);
    Vector3 SLIDE_SCALE = new Vector3(8.0f, 4.0f, 4.0f);

    Vector3 Velocity;
    float slidetimer;
    bool lookslikeweregonnahavetojump;
    bool justslide;
    bool unjump;
    bool OnTheGround;
    bool slidelocationadjustlock;

	// Use this for initialization
	void Start () {
	    
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            lookslikeweregonnahavetojump = true;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            justslide = true;
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            justslide = false;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            unjump = true;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Velocity = GetComponent<Rigidbody2D>().velocity;

        Vector2 Acceleration = Vector2.zero;
        Acceleration += new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f) * ACCEL_RATE;
        Debug.Log(Velocity);
        Acceleration.x -= (Velocity.x - 50.0f) * DECEL_RATE;

        if (Physics2D.Raycast(transform.localPosition - Vector3.right * transform.localScale.x / 2.0f, -Vector2.up, transform.localScale.y * 1.01f / 2.0f) ||
            Physics2D.Raycast(transform.localPosition + Vector3.right * transform.localScale.x / 2.0f, -Vector2.up, transform.localScale.y * 1.01f / 2.0f)) {
            OnTheGround = true;
        }

        if (OnTheGround) {
            if (lookslikeweregonnahavetojump) {
                Acceleration.y += JUMP_POWER;
                OnTheGround = false;
            }
        }

        if (unjump && !OnTheGround && Velocity.y > 0.0f) {
            GetComponent<Rigidbody2D>().velocity += UNJUMP_RATE * Physics2D.gravity * (Velocity.y/JUMP_POWER);
        }

        //transform.position += Velocity * Time.fixedDeltaTime;
        GetComponent<Rigidbody2D>().AddForce(Acceleration, ForceMode2D.Impulse);

        if (slidetimer > MAX_SLIDE_TIME) {
            justslide = false;
        }

        if (justslide) {
            if (OnTheGround) {
                slidetimer += Time.fixedDeltaTime;
                transform.localScale = SLIDE_SCALE;
                if (!slidelocationadjustlock) {
                    transform.localPosition = GetComponent<Rigidbody2D>().transform.localPosition +
                        -2.0f * Vector3.up;
                    slidelocationadjustlock = true;
                }
            }
        } else {
            slidetimer = 0.0f;
            transform.localScale = DEFAULT_SCALE;
            if (slidelocationadjustlock) {
                transform.localPosition = GetComponent<Rigidbody2D>().transform.localPosition +
                    2.0f * Vector3.up;
                slidelocationadjustlock = false;
            }
        }

        OnTheGround = false;
        lookslikeweregonnahavetojump = false;
        unjump = false;
	}

}
