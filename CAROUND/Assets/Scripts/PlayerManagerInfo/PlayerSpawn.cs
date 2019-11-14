using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    private GameObject Player;
    public Transform _PlayerSpawn;
    public List<Transform> PlayerSpawnList = new List<Transform>();
    //public PlayerManager _playerManager;
    private RoomInformation roominformation;
             private List<UserInfo> UserInfo;
    private string MyName;

    //public List<UserInfo> UserInfo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSpawnList[0] = GameObject.Find("PlayerSpawn_1").GetComponent<Transform>();
        PlayerSpawnList[1] = GameObject.Find("PlayerSpawn_2").GetComponent<Transform>();
        PlayerSpawnList[2] = GameObject.Find("PlayerSpawn_3").GetComponent<Transform>();
        PlayerSpawnList[3] = GameObject.Find("PlayerSpawn_4").GetComponent<Transform>();

        roominformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        //Player = Resources.Load("Classic_16") as GameObject;
        _PlayerSpawn = GetComponent<Transform>();
        UserInfo = roominformation.userInfoList;
        MyName = roominformation.MyName;
        ViewCar();
    }
    public void ViewCar()
    {
        //for (int i = 0; i < roominformation.userInfoArray.Length; i++)
        //{
        //    PlayerCar = MyName + "_" + PlayerSelectCarImg[type].sprite.name;

        //    //var test = roominformation.UserAndCarName.Keys.ToList();
        //    if (test[i] != MyName)
        //    {
        //        continue;
        //    }

        //    GameObject playerCar = PhotonNetwork.Instantiate("Cars/" + roominformation.UserAndCarName[test[i]]
        //                                                         .Replace(" ", ""),
        //        PlayerSpawnList[i].transform.position,
        //        Quaternion.identity);
        //    playerCar.name = test[i];
        //}
        string[] ab = new string[2];
        string[] caradd = new string[2];
        for (int i = 0; i < roominformation.userInfoArray.Length; i++)
        {
            string user="";
            if (roominformation.userInfoArray[i].StartsWith(roominformation.MyName))
            {
                user = roominformation.userInfoArray[i];
            }
            for (int  j = 0;   j<user.Length;  j++)
            {
                if (user[j] == '/')
                {
                    ab = user.Split(new char[] { '/' });
                    caradd = ab[1].Split(new char[] {'_'});
                    /*
                    if (ab[j] == MyName)
                    {
                        continue;
                    }
                    */
                    GameObject playerCar = PhotonNetwork.Instantiate("Cars/" + caradd[0]+"/"+caradd[1],
                        PlayerSpawnList[0].transform.position, Quaternion.identity);
                    playerCar.name = ab[0];
                    return;
                }
            }
            //123/Derby
            /*
            if (user[i] == '/')
            {
                ab = roominformation.userInfoArray[i].Split(new char[] {'/'});
                if (ab[i] == MyName)
                {
                    continue;
                }
                GameObject playerCar = PhotonNetwork.Instantiate("Cars/" + ab[1],
                        PlayerSpawnList[i].transform.position,Quaternion.identity);
                playerCar.name = ab[0];
            }
            */
        }
    }
}
