using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class RoomInformation : MonoBehaviourPunCallbacks
{
    
    private PlayFabManager playfabmanager;
    private PanelOnOff panelonoff;

   
    [Header("Login")]
    //private GameObject Login;

    [Header("Lobby")]
    //private GameObject Lobby;
    private GameObject[] RoomButtons_test;
    private Button[] LobbyButtons;

    [Header("RoomCreate")]
    //private GameObject RoomCreate;
    private Button RoomCreateComplete;

    [Header("Room")]
   // private GameObject Room;
    private Button[] RoomButtons;

    private Text ListText;

    [Header("RoomInside")]
    //private GameObject RoomInside;
    private Text[] PlayerNameArray = new Text[4];
    private Button GameStartButton;

    [Header("ETC")] private Text ServerState;

    public PhotonView PV;

    private List<RoomInfo> CurrentRoomList = new List<RoomInfo>();
    private int multiple;
    StringBuilder SelectRoomName = new StringBuilder();

    private bool[] bReadyCheck = new bool[4];

    private int MaxPlayer= 4;
    private bool Active = false;
    public bool MoveScene = false;
    void Awake()
    {
        
        Screen.SetResolution(960,540,false);
    }

    void Start()
    {
        playfabmanager = GameObject.Find("PlayFabManager").GetComponent<PlayFabManager>();
            panelonoff = GameObject.Find("PanelOnOff").GetComponent<PanelOnOff>();

            //Login = GameObject.FindWithTag("Login");
            //Lobby = GameObject.FindWithTag("Lobby");
            //RoomCreate = GameObject.FindWithTag("RoomCreate");
            //Room = GameObject.FindWithTag("Room");
            //RoomInside = GameObject.FindWithTag("RoomInside");

            LobbyButtons = GameObject.Find("Lobby").GetComponentsInChildren<Button>();
            RoomButtons = GameObject.Find("RoomList").GetComponentsInChildren<Button>();
            GameStartButton = GameObject.FindWithTag("GameStartButton").GetComponent<Button>();
            ServerState = GameObject.FindWithTag("ServerState").GetComponent<Text>();
            ListText = GameObject.FindWithTag("List").GetComponent<Text>();

            PlayerNameArray = GameObject.Find("RoomInside").GetComponentsInChildren<Text>();

            for (int i = 0; i < 4; i++)
                bReadyCheck[i] = false;


            for (int i = 0; i < 2; i++)
            {
                LobbyButtons[i].interactable = false;
            }
    }


    // Update is called once per frame
    void Update()
    {
        if(Active==true)
        ServerState.text = PhotonNetwork.NetworkClientState.ToString();
    }
    #region 서버 접속

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = playfabmanager.UserNickname;
        Debug.Log(playfabmanager.UserNickname);
    }

    public override void OnConnectedToMaster()
    {
        for (int i = 0; i < 2; i++)
        {
            LobbyButtons[i].interactable = true;
        }
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        CurrentRoomList.Clear();
        panelonoff.PanelOn("Lobby");
    }
    #endregion


    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }





    #region 방 생성

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName+"님의 방", new RoomOptions { MaxPlayers = (byte)MaxPlayer });
    }


    //public StringBuilder GetRoomName()
    //{
    //    //StringBuilder te = new StringBuilder();
    //    //return te.Append(transform.GetChild(0).GetComponent<Text>().text);
    //    //for (int i = 0; i < 6; i++)
    //    //{
    //    //    //if (RoomButtons[i].)
    //    //}
    //}
    //방 만들기 설정(맵고르기)
    public void CreateRoomSetting()
    {
        panelonoff.PanelOn("RoomCreate");


    }
    public void SetRoomName(string text)
    {
        SelectRoomName.Clear();
        SelectRoomName.Append(text);
    }

    public void Join()
    {
        panelonoff.PanelOn("Room");

    }
    public void JoinRoom()
    {
        if(SelectRoomName.ToString().Trim()!=string.Empty)
        PhotonNetwork.JoinRoom(SelectRoomName.ToString().Trim(), null);
    }

    public override void OnJoinedRoom()
    {
        panelonoff.PanelOn("RoomInside");
        tetst();
        PlayerNameArray[0].text=PhotonNetwork.NickName;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    
    #endregion

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal(otherPlayer);
    }

    void tetst()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            ListText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " +
                            PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
        }
    }
    void RoomRenewal(Player player)
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            ListText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " +
                            PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
        }

        for (int i = 0; i < MaxPlayer; i++)
        {
            if (player.NickName == PlayerNameArray[i].text)
            {
                PlayerNameArray[i].text = string.Empty;
                return;
            }
            else if (PlayerNameArray[i].text == string.Empty)
            {
                PlayerNameArray[i].text = player.NickName;
                return;
            }
        }
        
        if (!PhotonNetwork.IsMasterClient)
        {
            GameStartButton.interactable = false;
        }
    }

    void MyListRenewal()
    {
        multiple = 0;
        for (int i = 0; i < RoomButtons.Length; i++)
        {
            try
            {
                if (CurrentRoomList[i] == null)
                    return;
                RoomButtons[i].interactable = ((i < CurrentRoomList.Count) || ((int)CurrentRoomList[i].PlayerCount != 4))
                    ? true
                    : false;


                //RoomButtons[i].enabled = (i < CurrentRoomList.Count) ? true : false;

                RoomButtons[i].transform.GetChild(0).GetComponent<Text>().text =
                    (i < CurrentRoomList.Count) ? CurrentRoomList[i].Name : "";
                RoomButtons[i].transform.GetChild(1).GetComponent<Text>().text = (i < CurrentRoomList.Count)
                    ? CurrentRoomList[i].PlayerCount + "/" + CurrentRoomList[i].MaxPlayers
                    : "";
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!CurrentRoomList.Contains(roomList[i]))
                    CurrentRoomList.Add(roomList[i]);
                else
                {
                    CurrentRoomList[CurrentRoomList.IndexOf(roomList[i])] = roomList[i];
                }


            }
            else if (CurrentRoomList.IndexOf(roomList[i]) != -1)
                CurrentRoomList.RemoveAt(CurrentRoomList.IndexOf(roomList[i]));
        }
        MyListRenewal();
        //PhotonNetwork.
    }

    public void ReadyCheck()
    {
        
    }

    public void GoBackToLobby()
    {
        panelonoff.PanelOn("Lobby");


        PhotonNetwork.LeaveRoom();
        //RoomRenewal();
        tetst();
    }

    public void GameStart()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
 