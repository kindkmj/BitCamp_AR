using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//유저의 정보와 플레이어의 정보를 나누어야함
public class PlayerInfo : MonoBehaviour
{
    //차량 생성시 차량의 이름= 플레이어의 이름으로 치환해주어야함
    public InGamePlayUIManager inGamePlayUiManager;
    public static GameObject Player;
    public Ranking rankingManager;
    //public PlayerManager _playerManager;
    public GameCondition GameCondition;
    public float MyPosition { get; set; } = 0f;

    public int playerIndex = 0;
    // Start is called before the first frame update

    /// <summary>
    /// 게임정보에 관한 내용이며 게임 시작시 유저의 이름을 설정해준뒤 해당 게임오브젝트를 PlayerManager에서 유저 리스트에 넣어줌
    /// 그뒤 유저의 인덱스 값을 저장함 
    /// </summary>
    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "GameScene")
            return;
        //Player = GameObject.FindWithTag("PlayerCar");
//Player = this.gameObject;
//        Player.name 이름은 유저의 닉네임으로 설정
        //_playerManager.SetPlayerInfo(this.gameObject);
        //SetIndex(Player.name);
        //inGamePlayUiManager;
    }

    // Update is called once per frame
    // MyPosition으로 현재 거리를 계속해서 더해줌 추후에 랭킹 비교시 활용할 예정임
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "GameScene")
            return;
        MyPosition += SetMyPosition(); //각 구간별 거리는 구함.
        if (inGamePlayUiManager.GameUI.gameObject.activeSelf)
        {
            //SetRanking();
        }
    }

    /*
    private void SetRanking()
    {
        //가동 안되므로 다시짜야함 으이구
        if (_playerManager.UserList.Count != 0)
        {
            float index = 1000;
            for (int i = 0; i < _playerManager.UserList.Count; i++)
            {
                for (int j = 0; j < _playerManager.UserList.Count; j++)
                {
                    if (_playerManager.UserList[playerIndex].GetCheckDistance() < _playerManager.UserList[j].GetCheckDistance())
                    {
                        index += 1000;
                        break;
                    }
                }
            }

            _playerManager.UserList[playerIndex].SetRank(_playerManager.UserList[playerIndex].GetRank()+index);
        inGamePlayUiManager.RankText.text = _playerManager.UserList[playerIndex].GetRank().ToString();
        }
    }
    */
    private float SetMyPosition()
    {
        float position = 0f;
        var list = GameCondition.CheckPointDictionary.Keys.ToList();
        for (int i = 0; i < list.Count; i++)
        {
            if (GameCondition.CheckPointDictionary[list[i]] == true)
            {
                position = Vector3.Distance(this.gameObject.transform.position, list[i].transform.position);
                break;
            }
        }
        return position;
    }
    
    
    /*
    private void ViewInfo()
    {
        //        inGamePlayUiManager.TrackText= 
        for (int i = 0; i < _playerManager.UserList.Count; i++)
        {
            if (_playerManager.UserList[i].GetUserName().ToString().StartsWith(PlayerInfo.Player.gameObject.name))
            {
                
            }
        }
    }

    private void SetIndex(string name)
    {
        for (int i = 0; i < _playerManager.UserList.Count; i++)
        {
            if(_playerManager.UserList[i].GetUserName().ToString() == name)
            {
                playerIndex = i;
            }

        }
    }
    */
}
