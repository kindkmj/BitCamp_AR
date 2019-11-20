using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class Ranking : MonoBehaviourPunCallbacks ,IPunObservable
{

#region Variable

    public PlayerInfo PlayerInfo;
    //public PlayerManager _playerManager;
    private Dictionary<string,float> RankInfo = new Dictionary<string, float>();
    private RoomInformation _roomInformation;
    private float PlayerDistance = 0f;
    private Transform ReferencePoint;
    private Transform PlayerTransform;
    private Dictionary<string,float> PlayerDicData = new Dictionary<string, float>();
    public int MyRank=0;
    public bool RankSignal = false;
    #endregion



    #region Event

    private void Start()
    {
        _roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        ReferencePoint = GameObject.Find("CenterPoint").GetComponent<Transform>();
        PlayerTransform = GameObject.Find(_roomInformation.MyName).GetComponent<Transform>();

        AddUser(_roomInformation.MyName);
    }

    //마스터 클라이언트 유저는 유저들의 이름들을 모두 저장해야하고 생성되는 차량이 어떤 유저의 것인지 모두 기억해야함
    //그 데이터를 토대로 유저들의 이동거리를 지속적으로 업데이트 해주고
    //유저는 단순히 자신이 몇등인지만 받으면 됨.

    //항상 마스터 클라이언트만 데이터를 보내주고 
    //마스터 클라이언트가 아닌경우는 데이터를 읽기만 해야함
    //사용자도 데이터를 보내려면 punrpc를 이용해야 할것으로 보임.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    public void Updata()
    {
        if (RankSignal == true)
        {
            PlayerDistance = Vector3.Distance(PlayerTransform.transform.position, ReferencePoint.transform.position);
            SendDistanceData(_roomInformation+","+PlayerDistance);
        }
    }

    #endregion

    #region Function

    /// <summary>
    /// 전송할 데이터의 규격은 아이디,이동거리 로 할
    /// </summary>
    /// <param name="data">아이디,이동거리 의 데이터 가 전송됨</param>
    public void SendDistanceData(string data)
    {
        photonView.RPC("PunSendDistanceData",RpcTarget.All,data);
    }

    /// <summary>
    /// 아이디,이동거리로 들어온 데이터를 아이디 와 이동거리로 나눈뒤 PlayerDicData 에서 아이디의 키 값이 있을 경우에는 그 키의 데이터를 업데이트 해주며
    /// 같은키가 없을 경우에는 새로 등록해준뒤 그 등록해준 값에 업데이트 해줌
    /// </summary>
    /// <param name="data"></param>
    [PunRPC]
    public void PunSendDistanceData(string data)
    {
        string[] dataSplit = new string[100];
        dataSplit= data.Split(new char[1] {','});

        var PlayerKeyList = PlayerDicData.Keys.ToList();
        for (int i = 0; i < PlayerKeyList.Count; i++)
        {
            if (PlayerKeyList[i] == dataSplit[0])
            {
                PlayerDicData[dataSplit[0]] = PlayerDicData[dataSplit[0]] + float.Parse(dataSplit[1]);
                return;
            }
        }
        PlayerDicData.Add(dataSplit[0],float.Parse(dataSplit[1]));
    }

    /// <summary>
    /// 내 랭킹정보를 조회하는 항목.MyRank에는 내 랭킹점수를 기록함.
    /// </summary>
    public void SetRank()
    {
        var PlayerValueList = PlayerDicData.Values.ToList();
        var test = PlayerDicData[_roomInformation.MyName];
        int index = 0;
        for (int j = 0; j < PlayerValueList.Count; j++)
        {
            if (test < PlayerValueList[j])
            {
                index++;
            }
        }
        MyRank = index;
    }

    /// <summary>
    /// 내가 이동한 거리를 저장할 변수 해당 함수는 모든유저가 공통적으로 수행할 내용임
    /// </summary>
    private void UpdateDistance()
    {
        PlayerDistance = Vector3.Distance(PlayerTransform.transform.position, ReferencePoint.transform.position);
    }

    /// <summary>
    /// 마스터 클라이언트는 유저의 데이터를 지속적으로 업데이트 해줌
    /// </summary>
    /// <param name="_PlayerName">유저의 이름</param>
    /// <param name="_PlayerDistance">유저의 이동거리</param>
    private void UpdatePlayerDistance(string _PlayerName,float _PlayerDistance)
    {
        var RankKeyList = RankInfo.Keys.ToList();
        for (int i = 0; i < RankKeyList.Count; i++)
        {
            if (RankKeyList[i] == _PlayerName)
            {
                RankInfo[_PlayerName] = _PlayerDistance;
                break;
            }
        }
    }
    /// <summary>
    /// 랭킹을 비교하기 위한 유저를 등록함 
    /// 마스터 클라이언트 유저는 마스터클라이언트 유저가 아닌 유저들을 등록한뒤 관리하기 위함
    /// 거리 0f는 기본 초기값임
    /// </summary>
    private void AddUser(string _PlayerName)
    {
        RankInfo.Add(_PlayerName, 0f);
    }


    /// <summary>
    /// 유저의 거리를 지속적으로 업데이트 해줌??
    /// </summary>
//    private void SetUserDistance()
//    {
//        float UserDistance = _roomInformation.userInfoList[PlayerInfo.playerIndex].GetCheckDistance();
//        float UserRank = _roomInformation.userInfoList[PlayerInfo.playerIndex].GetRank();
//        //_playerManager.UserList[PlayerInfo.playerIndex].SetCheckDistance(PlayerInfo.MyPosition);
//        RankInfo.Add(_roomInformation.userInfoList[PlayerInfo.playerIndex].GetUserName().ToString(), UserDistance + UserRank);
//    }

    /// <summary>
    /// 유저의 랭킹을 정리해주는 함수
    /// </summary>
    /// <returns>리턴값으로 주는 List에는 유저의 순위가 저장되어 있음</returns>
    public List<string> RankingSetUp()
    {
        int[] rank = new int[_roomInformation.userInfoList.Count];
        List<string> RankKey = RankInfo.Keys.ToList();
        //랭킹 초기화
        for (int i = 0; i < rank.Length; i++)
        {
            rank[i] = 1;
        }
        //실질적인 랭킹 가리는 부분
        for (int i = 0; i < _roomInformation.userInfoList.Count; i++)
        {
            List<float> RankValue = RankInfo.Values.ToList();
            for (int j = 0; j < RankValue.Count; j++)
            {
                if (RankValue[i] < RankValue[j])
                {
                    rank[i]++;
                }
            }
        }

        //랭킹 인덱스 초기화 및 설정
        int[] index = new int[rank.Length];
        for (int i = 0; i < rank.Length; i++)
        {
            index[rank[i] - 1] = i;
        }

        List<string> Ranking = new List<string>();
        //랭킹 표출
        for (int i = 0; i < rank.Length; i++)
        {
            int t = index[i];
            Ranking.Add(RankKey[t]);
            //랭킹 보여주면 될듯.
            //RankKey[t];
        }

        return Ranking;
    }

    #endregion

    
}
