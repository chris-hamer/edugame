using UnityEngine;
using System.Collections;

public class imlazy : MonoBehaviour {

    public GameObject lvl1;
    public GameObject lvl2;
    public GameObject lvl3;
    public GameObject lvl4;
    public GameObject lvl5;
    public GameObject menu;
    public GameObject camera;
    
	// Use this for initialization
	void Start () {
        lvl1.SetActive(false);
        lvl2.SetActive(false);
        lvl3.SetActive(false);
        lvl4.SetActive(false);
        lvl5.SetActive(false);
        camera.SetActive(false);
        menu.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
