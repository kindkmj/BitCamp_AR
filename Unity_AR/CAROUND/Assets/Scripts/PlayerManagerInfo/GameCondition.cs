using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCondition : MonoBehaviour
{
    #region Variable
    public Dictionary<GameObject,bool> CheckPointDictionary = new Dictionary<GameObject, bool>();
    private GameObject[] CheckPoint;
    public bool[] CheckPointCondition;
    public float TrackCount = 0f;
    public bool TrackFlag = false;
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

    private void Update()
    {
        Debug.Log(TrackCount);
    }
    #endregion

    #region Function

    public void TrackCountControl()
    {
        InitDictionary();
    }

    public void InitDictionary()
    {
        TrackCount++;
        var list = CheckPointDictionary.Keys.ToList();
        for (int i = 0; i < list.Count; i++)
        {
            CheckPointDictionary[list[i]] = false;
        }
    }
    #endregion
}
