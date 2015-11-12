using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class characterlock : MonoBehaviour {

    public GameObject cherokee;
    public GameObject creek;
    public GameObject chicksaw;
    public GameObject seminole;
    public GameObject choctaw;

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        creek.GetComponent<Button>().interactable = false;
        foreach(Image i in creek.GetComponentsInChildren<Image>()) {
            i.color = Color.black;
        }
        seminole.GetComponent<Button>().interactable = false;
        foreach (Image i in seminole.GetComponentsInChildren<Image>()) {
            i.color = Color.black;
        }
        chicksaw.GetComponent<Button>().interactable = false;
        foreach (Image i in chicksaw.GetComponentsInChildren<Image>()) {
            i.color = Color.black;
        }
        choctaw.GetComponent<Button>().interactable = false;
        foreach (Image i in choctaw.GetComponentsInChildren<Image>()) {
            i.color = Color.black;
        }
        if (player.GetComponent<Player>().wins >= 1) {
            seminole.GetComponent<Button>().interactable = true;
            foreach (Image i in seminole.GetComponentsInChildren<Image>()) {
                i.color = Color.white;
            }
            chicksaw.GetComponent<Button>().interactable = true;
            foreach (Image i in chicksaw.GetComponentsInChildren<Image>()) {
                i.color = Color.white;
            }
        }
        if (player.GetComponent<Player>().wins >= 3) {
            choctaw.GetComponent<Button>().interactable = true;
            foreach (Image i in choctaw.GetComponentsInChildren<Image>()) {
                i.color = Color.white;
            }
            creek.GetComponent<Button>().interactable = true;
            foreach (Image i in creek.GetComponentsInChildren<Image>()) {
                i.color = Color.white;
            }
        }
	}
}
