using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private List<GameObject> PlayerList = new List<GameObject>();
    public List<UserInfo> UserList = new List<UserInfo>();

    private UserInfo FiUser;
    private UserInfo SeUser;
    private UserInfo ThUser;
    private UserInfo FoUser;


    //InGamePlayUIManager 에서 초기화를 진행함.
    public void initPlayerInfo()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (i == 0 && PlayerList[i] != null)
            {
                FiUser = new UserInfo(PlayerList[0], 0,false);
                UserList.Add(FiUser);
            }
            else if (i == 1 && PlayerList[i] != null)
            {
                SeUser = new UserInfo(PlayerList[1], 0, false);
                UserList.Add(SeUser);
            }
            else if (i == 2 && PlayerList[i] != null)
            {
                ThUser = new UserInfo(PlayerList[2], 0, false);
                UserList.Add(ThUser);
            }
            else if (i == 3 && PlayerList[i] != null)
            {
                FoUser = new UserInfo(PlayerList[3], 0, false);
                UserList.Add(FoUser);
            }
        }
    }
    //유저의 닉네임을 add해주기.
    public void SetPlayerInfo(GameObject _gameObject)
    {
        PlayerList.Add(_gameObject);
    }
}
