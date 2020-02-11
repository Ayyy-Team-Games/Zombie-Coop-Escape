using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using UnityEngine;

using System.Net;
using System.Net.Sockets;

public class GameManager : MonoBehaviour
{
    [Header("Multiplayer Server Settings")]
    [SerializeField]
    public string MultiplayerServerAddress;

    [SerializeField]
    public int MultiplayerServerPort;
    
    [SerializeField]
    public GameObject Spawner;

    [SerializeField]
    public GameObject NetworkPlayerPrefab;

    private IPEndPoint _multiplayerEndpoint;
    private GameObject _playerObject;
    private string _clientID;
    private UdpClient _client;

    private Dictionary<string, GameObject> _userTelemetry;
    

    // Start is called before the first frame update
    void Start()
    {
        _userTelemetry = new Dictionary<string, GameObject>();

        _client = new UdpClient();
        var multiplayerEndpoint = new IPEndPoint(IPAddress.Parse(MultiplayerServerAddress), MultiplayerServerPort);
        _client.Connect(multiplayerEndpoint);

        _multiplayerEndpoint = multiplayerEndpoint;
        _playerObject = GameObject.FindGameObjectWithTag("Player");

        var rnd = new System.Random();

        char[] alph = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        string name = "";
        for (int i = 0; i < 20; i++)
        {
            name += alph[rnd.Next(0, alph.Length)];
        }

        _clientID = name;
        Debug.Log("CLIENT ID!" + _clientID);

        // var newClone =;
    }
    
    // Update is called once per frame
    void Update()
    {
        string positions = String.Format("{0},{1},{2},{3},{4},{5},{6}",
            _clientID,
            _playerObject.transform.position.x,
            _playerObject.transform.position.y,
            _playerObject.transform.position.z,
            _playerObject.transform.rotation.x,
            _playerObject.transform.rotation.y,
            _playerObject.transform.rotation.z
        );
                
        byte[] sendBuffer = Encoding.ASCII.GetBytes(positions);
        _client.Send(sendBuffer, sendBuffer.Length);

        var recievedStream = _client.Receive(ref _multiplayerEndpoint);
        string recievedData = Encoding.ASCII.GetString(recievedStream, 0, recievedStream.Length);

        string[] users = recievedData.Split('|');
        foreach (var user in users) {
            string[] cords = user.Split(',');
            if (cords[0] == _clientID)
            {
                continue;
            }
            
            Vector3 updatedPosition = new Vector3(float.Parse(cords[1]), float.Parse(cords[2]), float.Parse(cords[3]));

            if (_userTelemetry.ContainsKey(cords[0]))
            {
                _userTelemetry[cords[0]].transform.position = updatedPosition;
                _userTelemetry[cords[0]].transform.rotation = new Quaternion(float.Parse(cords[4]), float.Parse(cords[5]), float.Parse(cords[6]), 1);
            } else
            {
                var newNetworkUser = Instantiate(NetworkPlayerPrefab, updatedPosition, Quaternion.identity);
                _userTelemetry.Add(cords[0], newNetworkUser);
            }
        }
    }
}
