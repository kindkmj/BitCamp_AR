using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCondition : MonoBehaviour
{
    #region Variable
    public PlayerManager _playerManager;

    public Dictionary<GameObject,bool> CheckPointDictionary = new Dictionary<GameObject, bool>();
    private GameObject[] CheckPoint;
  
    #endregion

    #region Event

    private void Start()
    {
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
        for (int i = 0; i < _playerManager.UserList.Count; i++)
        {
            if (_playerManager.UserList[i].GetUserName().ToString().StartsWith(PlayerInfo.Player.gameObject.name))
            {
                int index = _playerManager.UserList[i].GetCheckPointCount();
                _playerManager.UserList[i].SetCheckPointCount(++index);
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
