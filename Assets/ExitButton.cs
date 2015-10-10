using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Exit()
    {
        Application.Quit();
    }
}
