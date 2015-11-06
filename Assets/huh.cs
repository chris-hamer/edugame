using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class huh : MonoBehaviour {

    public GameObject player;
    public GameObject camera;
    public Button thing;
    public bool isplayer = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ItsTime()
    {
        GetComponent<Animator>().SetBool("horf", true);
    }

    public void Stop()
    {
        GetComponent<Animator>().SetBool("horf", false);
    }

    public void Move()
    {
        if (!isplayer) {
            thing.onClick.Invoke();
        } else {
            player.GetComponent<Player>().Respawn();
        }
    }

}
