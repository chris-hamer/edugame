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
    System.DateTime starttime;

	// Use this for initialization
	void Start () {
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

    public void Doit()
    {
        if (pass.text != password) {
            return;
        }
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string macAddress = "";

        foreach (NetworkInterface adapter in nics) {
            PhysicalAddress address = adapter.GetPhysicalAddress();
            byte[] bytes = address.GetAddressBytes();
            string mac = null;
            for (int i = 0; i < bytes.Length; i++) {
                mac = string.Concat(mac + (string.Format("{0}", bytes[i].ToString("X2"))));
                if (i != bytes.Length - 1) {
                    mac = string.Concat(mac + "-");
                }
            }

            macAddress += mac + "\n";
            break;

        }

        using (FileStream fs = new FileStream("Trail of Tears.csv", FileMode.Create)) {
            using (StreamWriter sw = new StreamWriter(fs)) {
                sw.WriteLine("Game Name: Trail of Tears");
                sw.WriteLine("MAC Address: " + macAddress);
                sw.WriteLine("Start Session: " + starttime.ToString());
                sw.WriteLine("End Session: " + System.DateTime.Now.ToString());
                sw.WriteLine("Game Progress: " + p.wins + " levels completed");
                sw.WriteLine("Rating: " + rating);
                sw.WriteLine("Total Game Time: " + (System.DateTime.Now-starttime).ToString());
                sw.Close();
            }

            fs.Close();
        }
    }
}
