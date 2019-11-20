using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class RewardSetUp : MonoBehaviourPunCallbacks
{
    public Text[] RankID;
    public Text[] RewardValues;
    void Start()
    {
        RankID = GameObject.Find("ViewRankId").GetComponentsInChildren<Text>();
        RewardValues = GameObject.Find("ViewRankId").GetComponentsInChildren<Text>();
    }

    public void ViewRank(string name)
    {
        photonView.RPC("PunViewRank", RpcTarget.All,name);
    }

    [PunRPC]
    public void PunViewRank( string name)
    {
        for (int i = 0; i < RankID.Length; i++)
        {
            if (RankID[i].text == null)
            {
                RankID[i].text = name;
            }
        }
    }
}
