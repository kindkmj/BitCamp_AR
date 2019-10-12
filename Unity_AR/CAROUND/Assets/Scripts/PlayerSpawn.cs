using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    private GameObject Car;

    public PhotonView pv;

    private int CarSpawnX = 10;
    //10.5,14,18,22 의 x좌표로 리스폰되야함.
    void Start()
    {
        Debug.Log(pv.ViewID);
        Car = Resources.Load("Classic_16") as GameObject;

        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                PhotonNetwork.Instantiate("Classic_16", new Vector3(CarSpawnX, 0f, 0f), Quaternion.identity);
                CarSpawnX += 4;
            }
        }
        
    }
}
