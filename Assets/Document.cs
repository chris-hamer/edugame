﻿using UnityEngine;
using System.Collections;

public class Document : MonoBehaviour {

    public string text;
    Vector3 originallocation;

	// Use this for initialization
	void Start () {
        Debug.Log(transform.localPosition);
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
	void Update () {
	}

}
