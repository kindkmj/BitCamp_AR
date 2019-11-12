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

public class RoomInformation : InitRoomScene, IPunObservable
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

    public Text UserText;

    private List<RoomInfo> CurrentRoomList = new List<RoomInfo>();
    private int multiple;
    StringBuilder SelectRoomName = new StringBuilder();

    private bool[] bReadyCheck = new bool[4];

    private int MaxPlayer = 4;
    private bool Active = false;
    public bool MoveScene = false;
    public List<UserInfo> userInfoList = new List<UserInfo>();
    public string[] userInfoArray;
    private bool bRoomManager = false;
    public string MyName;
    private string PlayerCar = "";
    public Image[] PlayerSelectCarImg;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Screen.SetResolution(960, 540, false);
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

    public void UserCountcheck()
    {
        //UserText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        UserText.text = userInfoList.Count.ToString();
    }



    #region 방 생성

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName + "님의 방", new RoomOptions {MaxPlayers = (byte) MaxPlayer});
        userInfoList.Add(new UserInfo(PhotonNetwork.NickName, 0, false, "DerbyCars"));
        PlayerNameArray[0].text = PhotonNetwork.NickName;
        bRoomManager = true;
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
        if (SelectRoomName.ToString().Trim() != string.Empty)
            PhotonNetwork.JoinRoom(SelectRoomName.ToString().Trim(), null);
    }

    public override void OnJoinedRoom()
    {
        panelonoff.PanelOn("RoomInside");
        //userInfoList.Add(new UserInfo(PhotonNetwork.NickName, 0, false, "DerbyCars"));
        MyName = PhotonNetwork.NickName;
        tetst();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    #endregion

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal(newPlayer);
        //SaveData(userInfoList);

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

        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < MaxPlayer; i++)
            {
                if (player.NickName == PlayerNameArray[i].text)
                {
                    for (int j = 0; j < userInfoArray.Length; j++)
                    {
                        if (player.NickName == userInfoArray[j])
                        {
                            userInfoArray[j] = string.Empty;
                            return;
                        }
                    }

//
//                    int index = 0;
//                    PlayerNameArray[i].text = string.Empty;
//                    for (int j = 0; j < userInfoList.Count; j++)
//                    {
//                        if (userInfoList[j].GetUserName() == player.NickName)
//                            index = j;
//
//                    }
//
//                    userInfoList.RemoveAt(index);
//                    return;
                }

                else if (PlayerNameArray[i].text == string.Empty && PlayerNameArray[i].text == null)
                {
                    PlayerNameArray[i].text = player.NickName;
                    for (int j = 0; j < PlayerNameArray.Length; j++)
                    {
                        if (userInfoArray[j] == string.Empty && userInfoArray[j] == null)
                        {
                            userInfoArray[j] = player.NickName;
                            return;
                        }
                    }

//                    userInfoList.Add(new UserInfo(player.NickName, 0, false, "DerbyCars"));
                }
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
                RoomButtons[i].interactable =
                    ((i < CurrentRoomList.Count) || ((int) CurrentRoomList[i].PlayerCount != 4))
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
        photonView.RPC("gamescence", RpcTarget.All);
    }

    [PunRPC]
    public void gamescence()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
//            userInfoArray = new UserInfo[userInfoList.Count - 1];
//            //userInfoArray = new UserInfo[userInfoList.Count];
//
//            for (int i = 0; i < userInfoArray.Length; i++)
//            {
//                userInfoArray[i] = userInfoList[i];
//            }
//            stream.SendNext(userInfoArray);
            stream.SendNext(userInfoArray);
            stream.SendNext(PlayerNameArray);
        }
        else
        {
            userInfoArray = (string[]) stream.ReceiveNext();
            PlayerNameArray = (Text[]) stream.ReceiveNext();
            //            userInfoArray = new UserInfo[userInfoList.Count - 1];
            //            //userInfoArray = new UserInfo[userInfoList.Count];
            //            userInfoArray = (UserInfo[])stream.ReceiveNext();
            //
            //            for (int i = 0; i < userInfoArray.Length; i++)
            //            {
            //                userInfoList[i] = userInfoArray[i];
            //            }
        }
    }


            //userInfoArray = new UserInfo[userInfoList.Count - 1];
            //userInfoArray = new UserInfo[userInfoList.Count];

    /// <summary>
    /// 마스터 클라이언트에 있는 메서드를 실행 유저의 정보를 저장하는데 이용함
    /// </summary>
    /// <param name="PlayerName">유저이름_유저의차량 으로 매개변수가 들어오며 배열에 저장이됨</param>
    [PunRPC]
    public void SetPlayerInfo(string PlayerName)
    {
        string ab = "";
        for (int i = 0; i < PlayerName.Length; i++)
        {
            if (PlayerName[i].ToString() == "_")
            {

            }

            ab = ab + PlayerName[i];
        }
        //PlayerName에서 데이터를 유저아이디_유저차량 으로 받게되며 받은 데이터를 유저아이디,차량 으로 나눈뒤
        //나눈 데이터중 기존의 유저아이디 와 같은 데이터를 찾은 뒤 해당 데이터를 유저아이디_유저차량으로 새롭게 업데이트
        //해줌으로써 유저가 선택한 차량으로 바꿈
//        for (int i = 0; i < userInfoArray.Length; i++)
//        {
//            userInfoArray = new UserInfo[userInfoList.Count - 1];
//            //userInfoArray = new UserInfo[userInfoList.Count];
//            userInfoArray = (UserInfo[])stream.ReceiveNext();
//
//            for (int i = 0; i < userInfoArray.Length; i++)
//            if (userInfoArray[i] == MyName)
//            {
//                userInfoArray[i] = userInfoArray[i] + "_" + "";
//            }
//        }
    }
    /// <summary>
    /// 차량이 보여지는 이미지를 누르게 되면 이미지 순서에따라서 type이 나눠지게 되고 눌러진 이미지의 스프라이트 이름을 저장 PlayerCar에 저장하여 사용함
    /// </summary>
    /// <param name="type">버튼이 눌러질때 누른 버튼이 어떤 타입인지 분간하기 위함</param>
    public void SetPlayerCar(int type)
    {
        if (type == 0)
        {
            PlayerCar = MyName +"_"+ PlayerSelectCarImg[type].sprite.name;
        }
        else if (type == 1)
        {
            PlayerCar = MyName + "_" + PlayerSelectCarImg[type].sprite.name;
        }
        else if (type == 2)
        {
            PlayerCar = MyName + "_" + PlayerSelectCarImg[type].sprite.name;
        }
        else if (type == 3)
        {
            PlayerCar = MyName + "_" + PlayerSelectCarImg[type].sprite.name;
        }
    }

    /// <summary>
    /// 유저가 준비버튼을 눌렀을때 실행됨
    /// </summary>
    public void PlayerReady()
    {
        PV.RPC("SetPlayerInfo",RpcTarget.MasterClient, PlayerCar);
    }
}
 
