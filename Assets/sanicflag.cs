using UnityEngine;
using System.Collections;

public class sanicflag : MonoBehaviour {

    public Player p;
    public Sprite repeat;
    public Sprite win;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (p.ndocs >= 6) {
            GetComponent<SpriteRenderer>().sprite = win;
        } else {
            GetComponent<SpriteRenderer>().sprite = repeat;
        }
	}
}
