using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour {
    [SerializeField] private string IPAddress;
    [SerializeField] private int port;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        UnityThread.initUnityThread();

        ClientHandleData.InitializePacketListener();
        ClientTCP.InitializeClientSocket(IPAddress, port);
       
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
