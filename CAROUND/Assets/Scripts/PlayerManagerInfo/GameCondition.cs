using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCondition : MonoBehaviour
{
    #region Variable
    //public PlayerManager _playerManager;

    public Dictionary<GameObject,bool> CheckPointDictionary = new Dictionary<GameObject, bool>();
    private GameObject[] CheckPoint;
    private RoomInformation _roomInformation;
  
    #endregion

    #region Event

    private void Start()
    {
        _roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        CheckPoint = GameObject.FindGameObjectsWithTag("CheckPoint");
        for (int i = 0; i < CheckPoint.Length; i++)
        {
            CheckPointDictionary.Add(CheckPoint[i],false);
        }
    }

    #endregion

    #region Function

    
    public void TrackCountControl()
    {
        InitDictionary();
    }

    /// <summary>
    /// 유저가 모든 체크포인트를 진입했을 경우 해당 유저의 트랙수를 늘린뒤 트랙진입 조건을 모두 초기화시켜줌
    /// </summary>
    public void InitDictionary()
    {

        for (int i = 0; i < _roomInformation.userInfoList.Count; i++)
        {
            if (_roomInformation.userInfoList[i].GetUserName().ToString().StartsWith(PlayerInfo.Player.gameObject.name))
            {
                int index = _roomInformation.userInfoList[i].GetCheckPointCount();
                _roomInformation.userInfoList[i].SetCheckPointCount(++index);
            }
        }
        var list = CheckPointDictionary.Keys.ToList();
        for (int i = 0; i < list.Count; i++)
        {
            CheckPointDictionary[list[i]] = false;
        }
    }
    #endregion
}
