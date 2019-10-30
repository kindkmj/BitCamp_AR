using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ranking : MonoBehaviour
{

#region Variable

    public PlayerInfo PlayerInfo;
    //public PlayerManager _playerManager;
    private Dictionary<string,float> RankInfo = new Dictionary<string, float>();
    private RoomInformation _roomInformation;
    #endregion



    #region Event

    private void Start()
    {
        _roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
    }
    #endregion

    #region Function

    /// <summary>
    /// 유저의 거리를 지속적으로 업데이트 해줌??
    /// </summary>
    private void SetUserDistance()
    {
        float UserDistance = _roomInformation.userInfoList[PlayerInfo.playerIndex].GetCheckDistance();
        float UserRank = _roomInformation.userInfoList[PlayerInfo.playerIndex].GetRank();
        //_playerManager.UserList[PlayerInfo.playerIndex].SetCheckDistance(PlayerInfo.MyPosition);
        RankInfo.Add(_roomInformation.userInfoList[PlayerInfo.playerIndex].GetUserName().ToString(), UserDistance + UserRank);
    }

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
