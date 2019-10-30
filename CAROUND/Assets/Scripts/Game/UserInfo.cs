using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    //랭킹의 단위는 1000으로 랭킹 + 거리를 비교하여 계산함.
    //UserName은 유저의 아이디
    //CheckPointCount 는 유저가 이동한 바퀴 수 
    //CheckDistance 는 유저가 이동한 거리
    //Rank는 유저의 랭킹을 표현
    //EndGame은 해당 유저의 경기가 끝이 낫는지 체크
    //EndDistance는 3바퀴를 돌았을때의 시간초?
    private string UserName;
    private int CheckPointCount = 0;
    private float CheckDistance = 0;
    private float Rank = 0;
    private bool EndGame;
    private float EndDistance;
    private string CarName;
    public Dictionary<GameObject, bool> CheckPointDictionary = new Dictionary<GameObject, bool>();

    public void SetCarName(string _CarName)
    {
        this.CarName = _CarName;
    }

    public string GetCarName()
    {
        return this.CarName;
    }
    public UserInfo(string _gameObject, float _endDistance, bool _endGame,string _CarName)
    {
        UserName = _gameObject;
        EndDistance = _endDistance;
        EndGame = _endGame;
        CarName = _CarName;
    }

    public float GetMyRecord()
    {
        return EndDistance;
    }

    public void SetMyRecord(float _endDistance)
    {
        EndDistance = _endDistance;
    }
    public bool GetEndGame()
    {
        return EndGame;
    }
    public void SetEndGame(bool _setEndGame)
    {
        EndGame = _setEndGame;
    }
    public float GetRank()
    {
        return Rank;
    }

    public void SetRank(float _rank)
    {
        Rank = _rank;
    }
    public string GetUserName()
    {
        return UserName;
    }

    #region initUser

    public int GetCheckPointCount()
    {
        return CheckPointCount;
    }
    public void SetCheckPointCount(int _CheckPointCount)
    {
        CheckPointCount = _CheckPointCount;
    }
    public float GetCheckDistance()
    {
        return CheckDistance;
    }
    public void SetCheckDistance(float _CheckDistance)
    {
        CheckDistance = _CheckDistance;
    }
    #endregion
}