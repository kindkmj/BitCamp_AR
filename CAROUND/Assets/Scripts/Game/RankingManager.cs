using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class RankingManager : MonoBehaviourPun
{
    public Dictionary<string,float> Ranking = new Dictionary<string, float>();
    private PhotonView pv;
    private  void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    public int UpdateRanking(string UserName,float  UserDistance)
    {
        var RankList = Ranking.Keys.ToList();
        var RankValueList = Ranking.Values.ToList();
        int index = 0;
        int rank = 1;
        for (int i = 0; i < RankList.Count; i++)
        {
            if (RankList[i] == UserName)
            {
                Ranking[UserName] = UserDistance;
                index = i;
            }

            for (int j = 0; j < RankValueList.Count; j++)
            {
                if (RankValueList[index] < RankValueList[j])
                {
                    rank++;
                }
            }
            break;
        }
        return rank;
    }
}
