using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private GameObject spawnerPlayerPrefrab;
    public bool StartGame = false;
    public string sceneNameToChange;
    public TextMeshProUGUI MaximosJugadoresGame;
    public TextMeshProUGUI ContadorJugadoresGame;
    public TextMeshProUGUI MaximosJugadoresPausa;
    public TextMeshProUGUI ContadorJugadoresPausa;
    public TextMeshProUGUI MAXJugadores_game;
    public TextMeshProUGUI MAXJugadores_pausa;
    public int MaxPlayers;
    public Transform PlayerSpawn;

    void Start()
    {
        ConnectedToServer();
    }


    private void Update()
    {
        #region //buscando jugadores viejo
        //GameObject[] players;
        //players = GameObject.FindGameObjectsWithTag("Player");
        //for (int i = 0; i < players.Length; i++)
        //{
        //    MaximosJugadoresGame.text = MaxPlayers.ToString();
        //    ContadorJugadoresGame.text = players.Length.ToString();
        //    MaximosJugadoresPausa.text = MaxPlayers.ToString();
        //    ContadorJugadoresPausa.text = players.Length.ToString();
        //}
        #endregion
    }

    void ConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        //print("Try connect to server...");
    }

    public override void OnConnectedToMaster()
    {
        //print("Connected to server.");
        base.OnConnectedToMaster();
        JoinOrCreateRoom();
    }

    void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        #region // Check if there is any room available
        //if (PhotonNetwork.CountOfRooms == 0)
        //{
        //    PhotonNetwork.CreateRoom("Sala1", roomOptions, TypedLobby.Default);
        //}
        //else
        //{
        PhotonNetwork.JoinOrCreateRoom("Sala1", roomOptions, TypedLobby.Default);

        //PhotonNetwork.JoinRandomRoom();
        //}
        #endregion
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // If joining a random room fails, create a new one
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Sala1", roomOptions, TypedLobby.Default);
    }

    public Transform[] puntosSpawn;
    public override void OnJoinedRoom()
    {
        //print("Joined on room.");
        base.OnJoinedRoom();
        string PJ_Select = PlayerPrefs.GetString("Personaje");
        Vector3 posToSet = puntosSpawn[PhotonNetwork.PlayerList.Length-1].position;
        spawnerPlayerPrefrab = PhotonNetwork.Instantiate(PJ_Select, posToSet, transform.rotation);
        CameraFollow.Instance.target = spawnerPlayerPrefrab.transform;
        CameraFollow.Instance.StartFollow();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Update player count when a new player enters the room
        ContadorJugadoresGame.text = PhotonNetwork.PlayerList.Length.ToString();
        ContadorJugadoresPausa.text = PhotonNetwork.PlayerList.Length.ToString();

        #region //viejo Check if the current client is the master client
        // Check if the current client is the master client
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    // If the room is full after the new player joined, close it and create a new one
        //    if (PhotonNetwork.PlayerList.Length == MaxPlayers)
        //    {
        //        PhotonNetwork.CurrentRoom.IsOpen = false;
        //        RoomOptions roomOptions = new RoomOptions();
        //        roomOptions.MaxPlayers = MaxPlayers;
        //        roomOptions.IsVisible = true;
        //        roomOptions.IsOpen = true;
        //        PhotonNetwork.JoinOrCreateRoom("Sala", roomOptions, null);
        //    }
        //}
        #endregion
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {


        GameObject[] currentCaballero = GameObject.FindGameObjectsWithTag("enemyKNIGHT");
        for (int i = 0; i < currentCaballero.Length; i++)
        {
            currentCaballero[i].GetComponent<EnemyKnightController>().UpdatePlayer();
            //Debug.Log(" + otherPlayer + " + currentCaballero.Length);

        }


        ContadorJugadoresGame.text = PhotonNetwork.PlayerList.Length.ToString();
        MAXJugadores_game.text = MaxPlayers.ToString();

        ContadorJugadoresPausa.text = PhotonNetwork.PlayerList.Length.ToString();
        MAXJugadores_pausa.text = MaxPlayers.ToString();
    }

    public void ChangeRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {

        //print("Left room");
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnerPlayerPrefrab);
        PhotonNetwork.Disconnect();
        ChanegScene();
    }

    public void ChanegScene()
    {
        PhotonNetwork.LoadLevel(sceneNameToChange);
    }
}