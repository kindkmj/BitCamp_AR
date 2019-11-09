using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    private GameObject Player;

    //public Transform _PlayerSpawn;
    private PhotonView pv;
    public List<GameObject> PlayerSpawnList = new List<GameObject>();
    public Ranking _Ranking;

    //public PlayerManager _playerManager;
    private RoomInformation roominformation;
    private List<UserInfo> UserInfo;
    private string MyName;

    //public List<UserInfo> UserInfo;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        roominformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        //Player = Resources.Load("Classic_16") as GameObject;
        //_PlayerSpawn = GetComponent<Transform>();
        UserInfo = roominformation.userInfoList;
        MyName = roominformation.MyName;
        ViewCar();
    }

    public void ViewCar()
    {
        for (int i = 0; i < roominformation.UserAndCarName.Count; i++)
        {
            var test = roominformation.UserAndCarName.Keys.ToList();
            if (test[i] != MyName)
            {
                continue;
            }

            GameObject playerCar = PhotonNetwork.Instantiate("Cars/" + roominformation.UserAndCarName[test[i]]
                                                                 .Replace(" ", ""),
                PlayerSpawnList[i].transform.position,
                Quaternion.identity);
            playerCar.name = test[i];
        }
    }
}

