using UnityEngine;
using System.Collections;

public class SlideCheck : MonoBehaviour {

    public bool cant;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LevelGeometry") {
            cant = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "LevelGeometry") {
            cant = false;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
