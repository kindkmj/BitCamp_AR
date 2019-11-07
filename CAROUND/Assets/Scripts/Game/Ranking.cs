using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class Ranking : MonoBehaviourPunCallbacks, IPunObservable
{

#region Variable
    
    public PhotonView pv;
    //private RankingManager _rankingManager;
    public PlayerInfo PlayerInfo;
    //public PlayerManager _playerManager;
    private Dictionary<string,float> RankInfo = new Dictionary<string, float>();
    private RoomInformation _roomInformation;
    private float Distance = 0f;
    private GameObject ReferencePoint;
    public int MyRank = 1; 

    //유저가 보낸 데이터를 읽을때 필요. 마스터 클라이언트만 작동
    private string UserData = "";
    //유저가 보낸 데이터를 배열로 저장
    private string[] UserDataArr = new string[4];
    //마스터 클라이언트가 보낸 문자열을 담을 배열
    private string[] DeliveredData = new string[4];
    //내 기록을 보낼 문자열
    private string MyData = "";
    #endregion



    #region Event

    private void Start()
    {
        //유저데이터 초기화 작업.
        for (int i = 0; i < UserDataArr.Length; i++)
        {
            UserDataArr[i] = string.Empty;
        }

        pv = GetComponent<PhotonView>();
        //_rankingManager = GameObject.Find("RankingManager").GetComponent<RankingManager>();
        _roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        ReferencePoint = GameObject.Find("CenterPoint");
    }

    private void Update()
    {
        for (int i = 0; i < UserDataArr.Length; i++)
        {
        Debug.Log(UserDataArr[i]);
        }
            Distance = GetDistance();
            if (UserDataArr[0] == string.Empty)
            {
                Debug.Log(1);
            }
            else
            {
                Debug.Log(UserDataArr[0]);
                Debug.Log(_roomInformation.MyName + "_" + Distance);
            }
            //pv.RPC("saverank",RpcTarget.MasterClient);
            //pv.RPC("");
    }

    #endregion

    #region Function

    public void AddPlayer()
    {
         
    }
//    [PunRPC]
//    private void saverank()
//    {
//        //내 랭크 점수를 기록함과 동시에 내 랭킹이 몇위인지 반환받는값.
//        MyRank = _rankingManager.UpdateRanking(_roomInformation.MyName,GetDistance());
//    }
//
//    private void saverank1(string username,float distance=0,int rank=0)
//    {
//        //내 랭크 점수를 기록함과 동시에 내 랭킹이 몇위인지 반환받는값.
//        MyRank = _rankingManager.UpdateRanking(_roomInformation.MyName, GetDistance());
//    }

    //자신의 거리를 체크하기
    private float GetDistance()
    {
        return (Distance += Vector3.Distance(this.gameObject.transform.position, ReferencePoint.transform.position));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    { 
//            //마스터 유저는
//            //유저들이 전달해주는 데이터를 가지고 랭킹을 계산하여 유저이름_랭킹순위 로 반환해줌
//        if (stream.IsWriting && PhotonNetwork.IsMasterClient)
//        {
//            //마스터 유저의 배열에는 마스터유저의 데이터를 넣어주어야함
//            UserDataArr[0] = _roomInformation.MyName + "_" + Distance;
//            //해당 항목이 비동기로 되는지 혹은 그렇지 않은지 모르겠음 유저가 4명이 있을때 마스터를 제외한 3명의 유저가 데이터를 전송한다면 3명의 데이터가 온전히 저장되는지 혹은 3명의 데이터가 들어온다 하더라도 한명의 데이터를 처리하므로 2명의 데이터를 처리하지 못한다던지 3명의 데이터가 모두 온전히 저장되지 않는지 잘 모르겠음 테스트가 필요해보임.
//            //만약 읽을 데이터가 있다면 (유저가 데이터를 전송했다면)
//            if (stream.IsReading)
//            {
//                UserData = (string) stream.ReceiveNext();
//                for (int i = 0; i < UserDataArr.Length; i++)
//                {
//                    //만약 유저데이터 배열의 시작문자열이 UserData로 들어온 문자열의 첫 문자열과 같다면 그 데이터의 거리를 업데이트 해줌
//                    if (UserDataArr[i].StartsWith(UserData.Remove(UserData.IndexOf('_'))))
//                    {
//                        UserDataArr[i] = UserData;
//                    }
//                    //그렇지 않은 새로운 데이터라면 새로운 문자열이므로 새 위치에 저장을 해줌
//                    else if (UserDataArr[i] == string.Empty)
//                    {
//                        UserDataArr[i] = UserData;
//                    }
//                }
//            }
//
//            // 데이터는 저장했으므로 저장된 데이터를 가지고 랭킹 작업을 해야함.
//            // 해당 데이터들의 순서가 어떻게 진행되는지 알수 없으므로 일단 로직을 짠뒤 테스트를 해보아야 할것으로 보임.
//            string[] RankData = new string[4];
//            UserRankProcess(ref RankData, UserDataArr);
//            stream.SendNext(RankData);
//        }
//        //마스터가 아닌 유저는 마스터에게 자신의이름_자신의 거리 를 주고 마스터에게서 유저들의 이름_랭킹순위 중에서 유저들의 이름중 자신의 이름과 같은 이름에 붙어있는 랭킹 순위를 자신의 순위로 지정한뒤 인게임매니저 즉 화면UI에 뿌려줌
//        else
//            {
//                //마스터가 아닌 유저는 문자열을 받게 되며 문자열에는 내이름_랭킹순위 로 반환된 데이터가 들어있음
//                if (stream.IsReading)
//                {
//                    DeliveredData = (string[]) stream.ReceiveNext();
//                }
//
//                for (int i = 0; i < DeliveredData.Length; i++)
//                {
//                    if (DeliveredData[i] == string.Empty)
//                        break;
//                    if (DeliveredData[i].StartsWith(_roomInformation.MyName))
//                    {
//                        Int32.TryParse(DeliveredData[i].Remove(0, DeliveredData[i].IndexOf('_') + 1), out MyRank);
//                    }
//                }
//
//                //MyRank가 0이면 형변환 실패이므로 데이터가 정상적으로 전달되지 않음
//                //MyRank가 0이 아니면 정상적으로 캐스팅된 데이터임
//                //마스터가 아닌 유저는 이름+자신의 이동거리 전달
//                stream.SendNext(_roomInformation.MyName + "_" + Distance);
//            }

        }
    

    private void UserRankProcess(ref string[] Userdatas,string[] Arrdatas)
    {
        float[] RankValue = new float[4];
        for (int i = 0; i < Arrdatas.Length; i++)
        {
            if (Arrdatas[i] == string.Empty)
                break;
            RankValue[i] = float.Parse(Arrdatas[i].Remove(0, Arrdatas[i].IndexOf('_') + 1));
        }
        for (int i = 0; i < RankValue.Length; i++)
        {
            if (Arrdatas[i] == string.Empty)
                break;
            int index = 1;
            for (int j = 0; j < RankValue.Length; j++)
            {
                if (RankValue[i] < RankValue[j])
                {
                    index++;
                }
            }
            Userdatas[i] = Arrdatas[i].Remove(Arrdatas[i].IndexOf('_')) + "_" + index;
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
