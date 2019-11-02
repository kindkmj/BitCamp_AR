using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class Ranking : MonoBehaviourPun, IPunObservable
{

#region Variable

    private PhotonView pv;
    private RankingManager _rankingManager;
    public PlayerInfo PlayerInfo;
    //public PlayerManager _playerManager;
    private Dictionary<string,float> RankInfo = new Dictionary<string, float>();
    private RoomInformation _roomInformation;
    private float Distance = 0f;
    private GameObject ReferencePoint;
    public int MyRank = 0;
    private string UserData = "";
    private string MyData = "";
    #endregion



    #region Event

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        _rankingManager = GameObject.Find("RankingManager").GetComponent<RankingManager>();
        _roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        ReferencePoint = GameObject.Find("CenterPoint");
        //랭킹매니저에 유저 추가
        _rankingManager.Ranking.Add(_roomInformation.MyName,0f);
    }

    private void Update()
    {
        pv.RPC("saverank",RpcTarget.MasterClient);
        pv.RPC("",);
    }
    #endregion

    #region Function

    [PunRPC]
    private void saverank()
    {
        //내 랭크 점수를 기록함과 동시에 내 랭킹이 몇위인지 반환받는값.
        MyRank = _rankingManager.UpdateRanking(_roomInformation.MyName,GetDistance());
    }

    private void saverank1(string username,float distance=0,int rank=0)
    {
        //내 랭크 점수를 기록함과 동시에 내 랭킹이 몇위인지 반환받는값.
        MyRank = _rankingManager.UpdateRanking(_roomInformation.MyName, GetDistance());
    }

    //자신의 거리를 체크하기
    private float GetDistance()
    {
        return Distance += Vector3.Distance(this.gameObject.transform.position, ReferencePoint.transform.position);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //마스터 유저는 유저들이 전달해주는 데이터를 가지고 랭킹을 계산하여 유저이름_랭킹순위 로 반환해줌
        if (stream.IsWriting && PhotonNetwork.IsMasterClient)
        {
            if (stream.IsReading)
            {
                UserData = (string) stream.ReceiveNext();
            }
            //stream.SendNext(_roomInformation.MyName + "_" + 이름에 관한 랭킹);
        }
        //마스터가 아닌 유저는 마스터에게 자신의이름_자신의 거리 를 주고 마스터에게서 유저들의 이름_랭킹순위 중에서 유저들의 이름중 자신의 이름과 같은 이름에 붙어있는 랭킹 순위를 자신의 순위로 지정한뒤 인게임매니저 즉 화면UI에 뿌려줌
        else
        {
            if (stream.IsReading)
            {
                MyData = (string) stream.ReceiveNext();
            }
            //마스터가 아닌 유저는 이름+자신의 이동거리 전달stream.SendNext(_roomInformation.MyName + "_" + Distance.ToString());
        }
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
    //
    //    /// <summary>
    //    /// 유저의 랭킹을 정리해주는 함수
    //    /// </summary>
    //    /// <returns>리턴값으로 주는 List에는 유저의 순위가 저장되어 있음</returns>
    //    public List<string> RankingSetUp()
    //    {
    //        int[] rank = new int[_roomInformation.userInfoList.Count];
    //        List<string> RankKey = RankInfo.Keys.ToList();
    //        //랭킹 초기화
    //        for (int i = 0; i < rank.Length; i++)
    //        {
    //            rank[i] = 1;
    //        }
    //        //실질적인 랭킹 가리는 부분
    //        for (int i = 0; i < _roomInformation.userInfoList.Count; i++)
    //        {
    //            List<float> RankValue = RankInfo.Values.ToList();
    //            for (int j = 0; j < RankValue.Count; j++)
    //            {
    //                if (RankValue[i] < RankValue[j])
    //                {
    //                    rank[i]++;
    //                }
    //            }
    //        }
    //
    //        //랭킹 인덱스 초기화 및 설정
    //        int[] index = new int[rank.Length];
    //        for (int i = 0; i < rank.Length; i++)
    //        {
    //            index[rank[i] - 1] = i;
    //        }
    //
    //        List<string> Ranking = new List<string>();
    //        //랭킹 표출
    //        for (int i = 0; i < rank.Length; i++)
    //        {
    //            int t = index[i];
    //            Ranking.Add(RankKey[t]);
    //            //랭킹 보여주면 될듯.
    //            //RankKey[t];
    //        }
    //
    //        return Ranking;
    //    }

    #endregion


}
