using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Photon.Pun;
using UnityEngine;

public class CheckPoint : MonoBehaviourPunCallbacks
{
    private PlayerManager _playerManager;
    private GameCondition gameCondition;
    private GameObject PlayerCar;
//    private List<`GameObject> CheckPointKeyList;
    private List<GameObject> CheckPositionList = new List<GameObject>();
    private GameObject[] checkarray;
    public Dictionary<string,bool> CheckDicData = new Dictionary<string, bool>();
    private RoomInformation _roomInformation;
    private csTouchMgr _csTouch;

    public string Myname;
//    private Ranking Rank;
    
    private void Start()
    {
        _roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        _playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        _csTouch = GameObject.Find("ARManager").GetComponent<csTouchMgr>();
        gameCondition = GameObject.FindGameObjectWithTag("ConditionManager").GetComponent<GameCondition>();
        Myname = _roomInformation.MyName;
//        CheckPointKeyList = gameCondition.CheckPointDictionary.Keys.ToList();
    }

    /// <summary>
    /// CheckPoint의 게임오브젝트의 자식데이터를 찾은뒤 데이터의 이름 값 , bool 형 값을 추가함
    /// </summary>
    private void InitSet()
    {
        checkarray = GameObject.Find("CheckPoint").GetComponentsInChildren<GameObject>();
        for (int i = 0; i < checkarray.Length; i++)
        {
            if (checkarray[i].name.StartsWith("CheckPoint_"))
            {
                CheckDicData.Add(checkarray[i].name,false);
            }
        }
    }


    /// <summary>
    /// 모든 체크포인트의 값이 true가 된다면 해당 유저의 바퀴수는 1 증가하게 된다.
    /// </summary>
    public void AllCheckPoint()
    {
        var DicValue = CheckDicData.Values.ToList();
        for (int i = 0; i < CheckDicData.Count; i++)
        {
            if (DicValue[i] != false)
            {
                return;
            }
        }

        _playerManager.PlayerCheckPointCount++;
        _playerManager.PlayerCountUpdate(_roomInformation.MyName, _playerManager.PlayerCheckPointCount);
        InitCheckPointCount();
    }

    public void InitCheckPointCount()
    {
        var DicKeys = CheckDicData.Keys.ToList();
        for (int i = 0; i < CheckDicData.Count; i++)
        {
            CheckDicData[DicKeys[i]] = false;
        }
    }

    /// <summary>
    /// 체크포인트는 기본값이 false이며 체크포인트를 지나치게되면 true로 값이 변경된다.
    /// </summary>
    /// <param name="TagName">체크포인트의 이름이며 각 체크포인트는 이름,bool로 관리하게된다</param>
    public void CountCheck(string TagName)
    {
        if (CheckDicData[TagName] == false)
        {
            CheckDicData[TagName] = true;
        }
    }
}

