using UnityEngine;
using System.Collections;

public class imlazy : MonoBehaviour {

    public GameObject lvl;
    public GameObject menu;
    public GameObject camera;
    
	// Use this for initialization
	void Start () {
        lvl.SetActive(false);
        camera.SetActive(false);
        menu.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
