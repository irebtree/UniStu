using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
public class TestSocket : MonoBehaviour {
    Socket sk;
    string ip = "192.168.0.1";
    int port = 9090;

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Connect()
    {
        sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        sk.Connect(ip, port);
    }
}
