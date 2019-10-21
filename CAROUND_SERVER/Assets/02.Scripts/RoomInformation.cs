using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class RoomInformation : MonoBehaviourPunCallbacks
{
    [Header("Lobby")]
    private GameObject Lobby;
    private GameObject[] RoomButtons_test;
    private Button[] LobbyButtons;

    [Header("RoomCreate")]
    private GameObject RoomCreate;
    private Button RoomCreateComplete;

    [Header("Room")]
    private GameObject Room;
    private Button[] RoomButtons;

    private Text ListText;

    [Header("RoomInside")]
    private GameObject RoomInside;
    private List<Text> PlayerNameList = new List<Text>();
    private Button GameStartButton;

    [Header("ETC")] private Text ServerState;

    public PhotonView PV;

    private List<RoomInfo> CurrentRoomList = new List<RoomInfo>();
    private int multiple;
    StringBuilder SelectRoomName = new StringBuilder();

    private bool[] bReadyCheck = new bool[4];

    void Awake()
    {
        Screen.SetResolution(960,540,false);
    }
    void Start()
    {

        Lobby = GameObject.FindWithTag("Lobby");
        RoomCreate = GameObject.FindWithTag("RoomCreate");
        Room = GameObject.FindWithTag("Room");
        RoomInside = GameObject.FindWithTag("RoomInside");

        LobbyButtons = GameObject.Find("Lobby").GetComponentsInChildren<Button>();
        RoomButtons = GameObject.Find("RoomList").GetComponentsInChildren<Button>();
        GameStartButton = GameObject.FindWithTag("GameStartButton").GetComponent<Button>();
        ServerState = GameObject.FindWithTag("ServerState").GetComponent<Text>();
        ListText = GameObject.FindWithTag("List").GetComponent<Text>();

        for(int i=0;i<4;i++)
            bReadyCheck[i] = false;

        Room.SetActive(false);
        RoomInside.SetActive(false);
        RoomCreate.SetActive(false);
        Debug.Log(RoomButtons.Length);
        Debug.Log(CurrentRoomList.Count);
        for (int i = 0; i < 2; i++)
        {
            LobbyButtons[i].interactable = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        ServerState.text = PhotonNetwork.NetworkClientState.ToString();
    }
    #region 서버 접속

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = Random.Range(1, 100).ToString();
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
    }
    #endregion


    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }





    #region 방 생성

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName+"님의 방", new RoomOptions { MaxPlayers = 4 });
        Lobby.SetActive(false);
        RoomInside.SetActive(false);
        RoomCreate.SetActive(false);

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
        Lobby.SetActive(false);
        RoomInside.SetActive(false);
        RoomCreate.SetActive(true);
        Room.SetActive(false);
    }
    public void SetRoomName(string text)
    {
        SelectRoomName.Clear();
        SelectRoomName.Append(text);
    }

    public void Join()
    {
        Room.SetActive(true);
        Lobby.SetActive(false);
        RoomCreate.SetActive(false);

        RoomRenewal();

    }
    public void JoinRoom()
    {
        Debug.Log(SelectRoomName.ToString().Trim());
        if(SelectRoomName.ToString().Trim()!=string.Empty)
        PhotonNetwork.JoinRoom(SelectRoomName.ToString().Trim(), null);
    }

    public override void OnJoinedRoom()
    {
        Room.SetActive(false);
        RoomInside.SetActive(true);
        RoomCreate.SetActive(false);

        RoomRenewal();
        //if (PhotonNetwork.CurrentRoom.SetMasterClient())
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    #endregion

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            ListText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " +
                            PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
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
                    Debug.Log(CurrentRoomList.Count);
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
        Room.SetActive(false);
        Lobby.SetActive(true);
        RoomInside.SetActive(false);
        PhotonNetwork.LeaveRoom();
        RoomRenewal();
    }
}
 