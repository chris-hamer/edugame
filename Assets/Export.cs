//////////

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class Export : MonoBehaviour {

    public InputField pass;
    public string password;
    public Player p;
    public int rating;
    public Slider s;
    public AudioSource bleep;
    System.DateTime starttime;

	// Use this for initialization
	void Start () {
        int x = 0;
        starttime = System.DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateRating()
    {
        rating = (int)s.value;
    }

    public void SetRating(int r)
    {
        rating = r;
    }

    private static string GetMacAddress()
    {
        // try to read the address from some file (this works on the Samsung Galaxy Tab 4 with Android 4.4.2)
        const string l_filePath = "/sys/class/net/wlan0/address"; // substitutions for wlan0: ip6gre0 ip6tnl0 lo p2p0 sit0 wlan0
        string l_contents = File.ReadAllText(l_filePath);
        //Console.Write("Read from \"" + l_filePath + "\": " + l_contents);
        return l_contents;
    }

    public void Doit()
    {
        if (pass.text != password) {
            return;
        }
        bleep.Play();
        string macAddress = GetMacAddress().Trim();

        using (FileStream fs = new FileStream("/storage/emulated/0/Documents/Trail of Tears.csv", FileMode.Append)) {
            using (StreamWriter sw = new StreamWriter(fs)) {
                string a = "Trail of Tears," + macAddress + "," + starttime.ToString() + "," + System.DateTime.Now.ToString() + "," + p.wins + "," + rating + "," + (System.DateTime.Now - starttime).ToString();
                sw.WriteLine(a);
                Debug.Log(a);
                //sw.WriteLine("Game Name: Trail of Tears");
                //sw.WriteLine("MAC Address: " + macAddress);
                //sw.WriteLine("Start Session: " + starttime.ToString());
                //sw.WriteLine("End Session: " + System.DateTime.Now.ToString());
                //sw.WriteLine("Game Progress: " + p.wins + " levels completed");
                //sw.WriteLine("Rating: " + rating);
                //sw.WriteLine("Total Game Time: " + (System.DateTime.Now-starttime).ToString());
                sw.Close();
            }

            fs.Close();
        }
    }
}
