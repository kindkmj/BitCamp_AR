using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class InGamePlayUIManager : MonoBehaviourPunCallbacks
{
    #region Variable

    public PhotonView pv;
    private Text CountDownText;
    //public PlayerManager _playerManager;

    //게임종료시 표현해주는 ui
    public GameObject RewardUI;
    public Text[] RewardTexts;

    public Ranking RankingObject;

    //게임진행에 필요한 ui
    public GameObject GameUI;
    public Text RankText;
    public Text TrackText;
    private Text[] Texts;
    private Time_AA timea;
    private GameCondition GameCondition;
    private CheckPoint checkPoint;
    private float TrackTime = 0f;
    private RoomInformation _roomInformation;
    private Ranking ranking;
    private bool Ready = false;

    #endregion

    void Start()
    {
        //랭킹넣어주기
        //ranking = GetComponent<Ranking>();
        _roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        pv = GetComponent<PhotonView>();
        //_playerManager.initPlayerInfo();
        GameCondition = GameObject.FindGameObjectWithTag("ConditionManager").GetComponent<GameCondition>();
        timea = GameObject.FindWithTag("Timer").GetComponent<Time_AA>();
        Texts = GameObject.FindWithTag("Canvas").GetComponentsInChildren<Text>();
        for (int i = 0; i < Texts.Length; i++)
        {
            if (Texts[i].name == "CountDownText")
            {
                CountDownText = Texts[i];
            }
            else if (Texts[i].name == "RankText")
            {
                RankText = Texts[i];
            }
            else if (Texts[i].name == "TrackText")
            {
                TrackText = Texts[i];
            }
        }
        GameUI.SetActive(false);
        RewardUI.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        if(GameUI.activeSelf)
        CountDown();
        //내 랭킹은 내가 관리함
        //RankText.text = ranking.MyRank.ToString();
    }
    #region Event
    #endregion
    #region Function
    private void CountDown()
    {
        CountDownText.text = timea.CountDownTime;
        Condition();
    }
    /// <summary>
    /// 유저가 게임이 종료되었는지 유저의 현재 바퀴수가 3일경우 게임 종료
    /// </summary>
    private void Condition()
    {
        for (int i = 0; i < _roomInformation.userInfoList.Count; i++)
        {
            if (_roomInformation.userInfoList[i].GetCheckPointCount() == 3)
            {
                TrackText.text = _roomInformation.userInfoList[i].GetCheckPointCount().ToString();
            }
            else
            {
                TrackText.text = _roomInformation.userInfoList[i].GetCheckPointCount().ToString();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mySequence"></param>
    private void GameOver(int mySequence)
    {
        _roomInformation.userInfoList[mySequence].SetMyRecord(float.Parse(timea.CountDownTime));
        _roomInformation.userInfoList[mySequence].SetEndGame(true);
        for (int i = 0; i < _roomInformation.userInfoList.Count; i++)
        {
            if (_roomInformation.userInfoList[i].GetEndGame() != true)
                return;
            else
            {
                //포톤으로 모든 유저에게 전달해주어야함.
//                ViewReward();
            }
        }
    }


//    private void ViewReward()
//    {
//        RewardUI.SetActive(true);
//        GameUI.SetActive(false);
//        List<int> nums = new List<int>();
//        List<string> _rank = new List<string>();
//        Dictionary<string,float> dictionary = new Dictionary<string, float>();
//        for (int i = 0; i < _playerManager.UserList.Count; i++)
//        {
//            dictionary.Add(_playerManager.UserList[i].GetUserName().ToString(), _playerManager.UserList[i].GetRank());
//        }
//
//        for (int i = 0; i < dictionary.Count; i++)
//        {
//            for (int j = 0; j < dictionary.Count; j++)
//            {
//
//                if (dictionary[_playerManager.UserList[0].GetUserName().ToString()] <
//                    dictionary[_playerManager.UserList[i].GetUserName().ToString()])
//                {
//                    _rank[j] = _playerManager.UserList[i].GetUserName().ToString();
//                }
//            }
//        }
//
//        for (int i = 0; i < _rank.Count; i++)
//        {
//            RewardTexts[i].text = _rank[i];
//        }
//        RewardSetUp rewardSetUp = new RewardSetUp();
//        rewardSetUp.settest(_rank);
////        rewardSetUp 
//    }

    /// <summary>
    /// 랭킹을 보여주며 유저에게 보상을 지급할 수 있도록
    /// </summary>
    private void ViewRank()
    {
        var PlayerRank = RankingObject.RankingSetUp();
        for (int i = 0; i < PlayerRank.Count; i++)
        {
            RewardTexts[i].text = PlayerRank[i];
            //순위에 걸맞게 보상을 지급해야하며 보상내용을 고정적으로 RewardTextsp[4]~[7] 까지의 금액을 지급할 수 있도로 설정
        }
    }

    //public void 
    
    #endregion
}
