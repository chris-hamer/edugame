﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

    void OnEnable()
    {
        transform.position = new Vector3(0.0f, 10.0f, -10.0f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(50.0f, 0.0f), ForceMode2D.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Pause()
    {
        GetComponent<Rigidbody2D>().Sleep();
    }

    public void Unpause()
    {
        if (GetComponent<Rigidbody2D>().IsSleeping()) {
            GetComponent<Rigidbody2D>().WakeUp();
            GetComponent<Rigidbody2D>().AddForce(new Vector2(50.0f, 0.0f), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
    }
}
