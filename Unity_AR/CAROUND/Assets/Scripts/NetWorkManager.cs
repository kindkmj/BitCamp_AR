﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    [Header("DisconnectPanel")]
    private GameObject DisconnectPanel;
    private InputField NickNameInput;

    [Header("LobbyPanel")]
    private GameObject LobbyPanel;
    private InputField RoomInput;
    private Text WelcomeText;
    private Text LobbyInfoText;
    private Button[] CellBtn;
    private Button PreviousBtn;
    private Button NextBtn;

    [Header("RoomPanel")]
    private GameObject RoomPanel;
    private Text ListText;
    private Text RoomInfoText;
    private Button PlayGame;

    [Header("ETC")]
    private Text StatusText;
    public PhotonView PV;

    private List<RoomInfo> myList = new List<RoomInfo>();
    private int currentPage = 1, maxPage, multiple;

    #region 서버연결
    
    void Start()
    {
        DisconnectPanel = GameObject.FindWithTag("DisconnectPanel");
        NickNameInput = GameObject.FindWithTag("NickNameInput").GetComponent<InputField>();
        LobbyPanel = GameObject.FindWithTag("LobbyPanel");
        RoomInput = GameObject.FindWithTag("RoomInput").GetComponent<InputField>();
        WelcomeText = GameObject.FindWithTag("WelcomeText").GetComponent<Text>();
        LobbyInfoText = GameObject.FindWithTag("LobbyInfoText").GetComponent<Text>();
        CellBtn = GameObject.FindWithTag("CellBtn").GetComponents<Button>();
        PreviousBtn = GameObject.FindWithTag("PreviousBtn").GetComponent<Button>();
        NextBtn = GameObject.FindWithTag("NextBtn").GetComponent<Button>();
        RoomPanel = GameObject.FindWithTag("RoomPanel");
        ListText = GameObject.FindWithTag("ListText").GetComponent<Text>();
        RoomInfoText = GameObject.FindWithTag("RoomInfoText").GetComponent<Text>();
        StatusText = GameObject.FindWithTag("StatusText").GetComponent<Text>();
        PlayGame = GameObject.FindWithTag("PlayGame").GetComponent<Button>();

        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " +
                             PhotonNetwork.CountOfPlayers + "접속";
    }
    public void Connect() =>PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() =>PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        DisconnectPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        WelcomeText.text = PhotonNetwork.LocalPlayer.NickName + "님 환영합니다";
        myList.Clear();
    }

    public void JoinRoom()
    {

    }
    public void Disconnect() =>PhotonNetwork.Disconnect();
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        DisconnectPanel.SetActive(true);

    }
    #endregion
    #region 방

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + Random.Range(0, 100) : RoomInput.text,
            new RoomOptions {MaxPlayers = 4});
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        RoomPanel.SetActive(true);
        DisconnectPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        if (!PhotonNetwork.IsMasterClient)
            PlayGame.interactable=false;
        RoomRenewal();

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
        CreateRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
        CreateRoom();
    }

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
            ListText.text += PhotonNetwork.PlayerList[i].NickName +
                             ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
            RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount +
                                "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
        }
    } 
    
    [PunRPC]
    public void GameJoin()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
          PhotonNetwork.LoadLevel("Prototype");
          photonView.RPC("GameJoin", RpcTarget.Others);
        //}
    }
    #endregion
    #region 방리스트 갱신
    // ◀버튼 -2,▶버튼=-1,셀 숫자
    public void MyListClick(int num)
    {
        if (num == -2)
            --currentPage;
        else if (num == -1)
            ++currentPage;
        else
            PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }

    void MyListRenewal()
    {
        // 최대 페이지
        maxPage = (myList.Count % CellBtn.Length == 0)
            ? myList.Count / CellBtn.Length
            : myList.Count / CellBtn.Length + 1;

        //이전, 다음 버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text =
                (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text =
                (multiple + i < myList.Count)
                    ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers
                    : "";
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if(!myList.Contains(roomList[i]))
                    myList.Add(roomList[i]);
                else
                    myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i])!=-1)
                myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    #endregion
    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : "+PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : "+PhotonNetwork.CurrentRoom.PlayerCount );
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);
            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            }
            print(playerStr);
        }
        else
        {
            print("접속한 인원 수 : " +PhotonNetwork.CountOfPlayers);
            print("방 개수 : "+PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : "+PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지 ? : "+PhotonNetwork.InLobby);
            print("연결됐는지? : "+PhotonNetwork.IsConnected);
        }
    }
}
