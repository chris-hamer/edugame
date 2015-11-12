using UnityEngine;
using System.Collections;

public class menumusicscript : MonoBehaviour {

    public GameObject menu;
    public GameObject charsel;
    public GameObject export;
    public GameObject rate;
    public Player p;

    public AudioSource music;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!(menu.activeSelf || charsel.activeSelf || export.activeSelf || rate.activeSelf || p.keepmenumusicon)) {
            music.Stop();
        } else if (!music.isPlaying) {
            music.Play();
        }
	}
}
