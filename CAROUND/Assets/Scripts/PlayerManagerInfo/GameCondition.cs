using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class GameCondition : MonoBehaviourPunCallbacks
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

    }
#endregion

}
