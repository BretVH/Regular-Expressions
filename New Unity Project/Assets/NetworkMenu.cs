using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour
{
    public string connectionIP = "127.0.0.1";
    public int portNumber = 65009;
    private bool connected = false;
    private string[] gameNames;
    private HostData[] hostData;
    public string playerName = "Player name";
    private bool refreshing = false;
    public string gameName = "test game";
    GameObject playerPrefab;
    public string serverName = "";
    private float wait;


    private void start()
    {
        playerName = PlayerPrefs.GetString("Player Name");
    }
    private void Update()
    {
        if (refreshing)
        {
            if (MasterServer.PollHostList().Length > 0)
            {
                refreshing = false;
                hostData = MasterServer.PollHostList();
            }
        }
    }
    private void OnConnectedToServer()
    {
        //client has connected
        connected = true;
    }

    private void OnServerInitialized()
    {
        //Server initialized
        connected = true;
        MasterServer.RegisterHost("Space Bomber", "Space Bomber!", "a game");
        DontDestroyOnLoad(transform.gameObject);
        Application.LoadLevel("nLvl1");
    }

    private void OnDisconnectedFromServer()
    {
        //connection lost
        connected = false;
    }

    private void OnMasterServerEvent(MasterServerEvent e)
    {
        if (e == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered Server");
        }
    }

    private void OnGUI()
    {
        if (!connected)
        {
            connectionIP = GUILayout.TextField(connectionIP);
            int.TryParse(GUILayout.TextField(portNumber.ToString()), out portNumber);
            GUILayout.Label("Your name : ");
            playerName = GUILayout.TextField(playerName);
            if (GUILayout.Button("Create Server"))
            {
                Network.InitializeServer(4, portNumber, !Network.HavePublicAddress());
                
                wait = Time.time;
            }
            if (GUILayout.Button("Refresh Available Games"))
            {
                if( Time.time - wait > 5.0f )
                   hostData = MasterServer.PollHostList();
            }
            if (hostData != null)
            {
                for (int i = 0; i < hostData.Length; i++)
                {
                    if (GUILayout.Button(hostData[i].gameName))
                    {
                        Network.Connect(hostData[i]);
                    }
                }
            }
        }
        else
        {
            GUILayout.Label("Connections: " + Network.connections.Length.ToString());
         
        }
    }
}
