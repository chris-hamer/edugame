using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    float ACCEL_RATE = 10.0f;
    float DECEL_RATE = 0.175f;
    float JUMP_POWER = 100.0f;
    float UNJUMP_RATE = 0.25f;
    float CENTERING_FORCE_SCALE = 1.5f;
    float MAX_SLIDE_TIME = 0.5f;

    public Sprite standsprite;
    public Sprite slidesprite;

    public AudioSource snd_jump;
    public AudioSource snd_slide;
    public AudioSource snd_fall;
    public AudioSource snd_get;

    public GameObject doit;

    public GameObject maincamera;
    public CanvasGroup docdisplay;
    public Text doctext;
    public Image docimage;
    public CanvasGroup quizdisplay;
    public Text quiztext;
    public Text quiza;
    public Text quizb;
    public Text quizc;
    public Text quizd;
    public Button goback;
    public int wins = 0;
    int correct;
    Quiz currentquiz;
    public int ndocs;
    bool sanic;
    float endofleveltimer;

    public GameObject sw2;

    Vector3 DEFAULT_SCALE = new Vector3(0.7f,1.4f,1.0f);
    Vector3 SLIDE_SCALE = new Vector3(0.7f, 0.7f, 1.0f);

    Vector3 Velocity;
    Vector3 oldvelocity;
    float slidetimer;
    bool lookslikeweregonnahavetojump;
    bool justslide;
    bool unjump;
    bool OnTheGround;
    bool slidelocationadjustlock;
    bool cantstopwontstop;
    bool zawarudo;

	// Use this for initialization
	void Start () {
	}

    void OnEnable()
    {
        sanic = false;
        correct = 0;
        endofleveltimer = -1.0f;
        ndocs = 0;
        transform.position = new Vector3(-42.0f, 75.0f, 0.0f);
    }

    public void CharSelect(int n)
    {
        GetComponent<Animator>().SetInteger("Character", n);
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
    
    public void Pause()
    {
        oldvelocity = GetComponent<Rigidbody2D>().velocity;
        zawarudo = true;
        GetComponent<Rigidbody2D>().Sleep();
        maincamera.GetComponent<CameraMove>().Pause();
    }

    public void QuizUnpause(string selected)
    {
        if (selected == currentquiz.correct) {
            currentquiz.Hide();
            correct++;
            if (correct >= 5) {
                wins++;
                sw2.GetComponent<huh>().ItsTime();
            }
        } else {
            currentquiz.timer = 0.0f;
        }
        Unpause();
    }

    public void Unpause()
    {
        snd_get.Stop();
        zawarudo = false;
        GetComponent<Rigidbody2D>().WakeUp();
        maincamera.GetComponent<CameraMove>().Unpause();
        GetComponent<Rigidbody2D>().velocity = oldvelocity;
        docdisplay.alpha = 0.0f;
        docdisplay.blocksRaycasts = false;
        docdisplay.interactable = false;
        quizdisplay.alpha = 0.0f;
        quizdisplay.blocksRaycasts = false;
        quizdisplay.interactable = false;
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
        if (Input.GetKeyDown(KeyCode.Return)) {
            Unpause();
        }
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (endofleveltimer >= 0.0f) {
            endofleveltimer += Time.fixedDeltaTime;
        }
        if (endofleveltimer > 3.0f) {
            sw2.GetComponent<huh>().ItsTime();
        }
        if (zawarudo) {
            GetComponent<Animator>().SetBool("WRYYYYYYYYYY", zawarudo);
            return;
        }

        Velocity = GetComponent<Rigidbody2D>().velocity;
        Vector2 Acceleration = Vector2.zero;
        float test = maincamera.transform.position.x - transform.position.x;
        Acceleration += new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f) * ACCEL_RATE;
        if (!sanic) {
            Acceleration.x -= (Velocity.x - 50.0f - test * CENTERING_FORCE_SCALE) * DECEL_RATE;
        }

        if (Physics2D.Raycast(transform.localPosition + (Vector3)GetComponent<BoxCollider2D>().offset * 8.0f - Vector3.right * transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2.0f + -Vector3.up * transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2.0f, -Vector2.up, 0.75f) ||
            Physics2D.Raycast(transform.localPosition + (Vector3)GetComponent<BoxCollider2D>().offset * 8.0f + Vector3.right * transform.localScale.x * GetComponent<BoxCollider2D>().size.x / 2.0f + -Vector3.up * transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2.0f, -Vector2.up, 0.75f)) {
            OnTheGround = true;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -30.0f);
            Velocity.y = -50.0f;
        }

        if (OnTheGround) {
            if (lookslikeweregonnahavetojump) {
                snd_jump.Play();
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
                //Velocity.y = 0.0f;
                Acceleration.y = JUMP_POWER;
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
                    snd_slide.Play();
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
                snd_slide.Stop();
                GetComponents<BoxCollider2D>()[0].offset += new Vector2(0.0f, SLIDE_SCALE.y / 2.0f);
                slidelocationadjustlock = false;
            }
        }

        GetComponent<Animator>().SetFloat("Velocity", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        GetComponent<Animator>().SetBool("Jump", !OnTheGround || lookslikeweregonnahavetojump);
        GetComponent<Animator>().SetBool("Slide", justslide);
        GetComponent<Animator>().SetBool("WRYYYYYYYYYY", zawarudo);

        OnTheGround = false;
        lookslikeweregonnahavetojump = false;
        unjump = false;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Collectable") {
            if (!other.gameObject.GetComponent<Document>().DisableSound) {
                snd_get.Play();
            }
            ndocs++;
            Pause();
            docdisplay.alpha = 1.0f;
            docdisplay.blocksRaycasts = true;
            docdisplay.interactable = true;
            //doctext.text = other.gameObject.GetComponent<Document>().text;
            docimage.sprite = other.gameObject.GetComponent<Document>().image;
            other.gameObject.GetComponent<Document>().Hide();
        }
        if (other.gameObject.tag == "Quiz") {
            currentquiz = other.gameObject.GetComponent<Quiz>();
            Pause();
            quizdisplay.alpha = 1.0f;
            quizdisplay.blocksRaycasts = true;
            quizdisplay.interactable = true;
            quiztext.text = other.gameObject.GetComponent<Quiz>().question;
            quiza.text = other.gameObject.GetComponent<Quiz>().a;
            quizb.text = other.gameObject.GetComponent<Quiz>().b;
            quizc.text = other.gameObject.GetComponent<Quiz>().c;
            quizd.text = other.gameObject.GetComponent<Quiz>().d;
        }
        if (other.gameObject.tag == "Getback") {
            if (ndocs >= 5) {
                wins++;
                maincamera.GetComponent<CameraMove>().sanic();
                sanic = true;
                endofleveltimer = 0.0f;
            } else {
                transform.position = new Vector3(transform.position.x, -100.0f, transform.position.z);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inbounds" && !sanic) {
            doit.GetComponent<huh>().ItsTime();
        }
    }

    public void Respawn()
    {
        transform.position = new Vector3(-42.0f, 75.0f, 0.0f);
        maincamera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }

}
