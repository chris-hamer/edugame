using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    void OnEnable()
    {
        transform.position = new Vector3(0.0f, 0.0f, -10.0f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(50.0f, 0.0f), ForceMode2D.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
    }
}
