using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCondition : MonoBehaviour
{
    #region Variable
    private GameObject[] CheckPoint;
    public bool[] CheckPointCondition;
    public float TrackCount = 0f;
    #endregion

    #region Event

    private void Start()
    {
        CheckPoint = GameObject.FindGameObjectsWithTag("CheckPoint");
    }

    public void InitCheckPointCondition()
    {
        CheckPointCondition = new bool[CheckPoint.Length];
        for (int i = 0; i < CheckPointCondition.Length; i++)
        {
            CheckPointCondition[i] = false;
        }
        TrackCount++;
    }

    #endregion

    #region Function

    #endregion
}
