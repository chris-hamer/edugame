using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

    GameObject player;
    public Export aa;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Exit()
    {
        aa.bypass = true;
        aa.Doit();
        Application.Quit();
    }
}
