using UnityEngine;
using System.Collections;

public class Quiz : MonoBehaviour {

    public string question;
    public string a;
    public string b;
    public string c;
    public string d;
    public string correct;
    public float timer=-1.0f;
    Vector3 originallocation;

    // Use this for initialization
    void Start()
    {

    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0) {
            GetComponent<CircleCollider2D>().enabled = false;
            timer += Time.deltaTime;
        }
        if (timer > 2.0f) {
            timer = -1.0f;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
