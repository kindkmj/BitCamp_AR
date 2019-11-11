using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    private GameObject Player;
    public Transform _PlayerSpawn;
    public List<GameObject> PlayerSpawnList = new List<GameObject>();
    //public PlayerManager _playerManager;
    private RoomInformation roominformation;
             private List<UserInfo> UserInfo;
    private string MyName;

    //public List<UserInfo> UserInfo;
    // Start is called before the first frame update
    void Start()
    {
        roominformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        //Player = Resources.Load("Classic_16") as GameObject;
        _PlayerSpawn = GetComponent<Transform>();
        UserInfo = roominformation.userInfoList;
        MyName = roominformation.MyName;
        ViewCar();
    }
    public void ViewCar()
    {
        for (int i = 0; i < UserInfo.Count; i++)
        {
            if (UserInfo[i].GetUserName() == MyName)
            {
                string CarColor = "";
                int a = Random.Range(0, 3);
                if (a == 0)
                {
                    CarColor = "Black";
                }
                else if (a == 1)
                {
                    CarColor = "Blue";
                }
                else if (a == 2)
                {
                    CarColor = "Green";
                }
                else if (a == 3)
                {
                    CarColor = "LightBlue";
                }

                string test = "Cars/" + UserInfo[i].GetCarName() + "_" + CarColor;
                GameObject UserCar = Resources.Load("Cars/" + UserInfo[i].GetCarName() + "/" + CarColor) as GameObject;
                Debug.Log(UserCar.name);
                PhotonView.Instantiate(UserCar, PlayerSpawnList[i].transform.position, Quaternion.identity);
            }
        }
    }
}
