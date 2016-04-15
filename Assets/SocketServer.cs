using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;

public class SocketServer : MonoBehaviour {
    string ip = "192.168.0.109";
    int port = 9080;
	// Use this for initialization
	void Start () {
        Test();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Test()
    {
        IPAddress ia = IPAddress.Parse(ip);
        IPEndPoint iep = new IPEndPoint(ia, port);

        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        server.Bind(iep);
        server.Listen(5);
        Debug.Log("waiting for a client..." );
        Socket client = server.Accept();

        IPEndPoint clientep =(IPEndPoint)client.RemoteEndPoint;

        Debug.Log("connect with : " + clientep.Address + "at : " + clientep.Port);

     }
}
