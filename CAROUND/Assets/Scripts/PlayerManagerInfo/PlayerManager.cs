using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private RewardSetUp _rewardSetUp;
    private CheckPoint _CheckPoint;
    public int PlayerCheckPointCount = 0;
    public Dictionary<string, int> DicPlayerCheckPoint = new Dictionary<string, int>();
    public Dictionary<string, int> DicPlayerRank = new Dictionary<string, int>();
    public List<string> RankList = new List<string>();
    private bool Signal=false;
    private void Start()
    {
        _CheckPoint = GameObject.Find("").GetComponent<CheckPoint>();
        _rewardSetUp = GameObject.Find("").GetComponent<RewardSetUp>();
    }

    /// <summary>
    /// 3바퀴를 돌았는지 지속적으로 체크를함
    /// </summary>
    private void Update()
    {
        if (Signal == true)
        {
            var DicKeys = DicPlayerCheckPoint.Keys.ToList();
            for (int i = 0; i < DicKeys.Count; i++)
            {
                if (DicKeys[i] == _CheckPoint.Myname)
                {
                    if (DicPlayerCheckPoint[_CheckPoint.Myname] == 3)
                    {
                        _rewardSetUp.ViewRank(_CheckPoint.Myname);
                        Signal = false;
                    }
                }
                else if (DicPlayerCheckPoint[_CheckPoint.Myname] != 3)
                {
                    Signal = true;
                }
            }
        }
        //게임종료 문구 및 보상화면으로 이동
    }

    public void PlayerCountUpdate(string data, int point)
    {
        photonView.RPC("PunPlayerCountUpdate", RpcTarget.All,data,point);
    }
    [PunRPC]
    public void PunPlayerCountUpdate(string data,int point)
    {
        var test = DicPlayerCheckPoint.Keys.ToList();
        for (int i = 0; i < DicPlayerCheckPoint.Count; i++)
        {
            if(test[i]==data)
            {
                DicPlayerCheckPoint[test[i]] = point;
                return;
            }
        }
        DicPlayerCheckPoint.Add(data,point);
    }

}
