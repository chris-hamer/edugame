using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class password : MonoBehaviour {

    public string pass;
    public InputField p;
    public GameObject pscreen;
    public GameObject menu;

	// Use this for initialization
	void Start () {
	
	}

    public void validate()
    {
        if (p.text.ToLower() == pass.ToLower()) {
            menu.SetActive(true);
            pscreen.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
