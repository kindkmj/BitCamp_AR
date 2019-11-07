using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindGameObject : MonoBehaviourPunCallbacks
{
    public RoomInformation _RoomInformation;
    public MapSetting _MapSetting;
    public PlayerInfo _PlayerInfo;
    public GameCondition _GameCondition;
    public InGamePlayUIManager _InGamePlayUiManager;
    public CheckPoint _CheckPoint;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (SceneManager.GetActiveScene().name == "ROOM")
        {
            _RoomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            _InGamePlayUiManager = GameObject.Find("InGameManager").GetComponent<InGamePlayUIManager>();
            _CheckPoint = GameObject.Find("CheckPoint").GetComponent<CheckPoint>();
            _GameCondition = GameObject.Find("InGameManager").GetComponent<GameCondition>();
        }
    }
}
