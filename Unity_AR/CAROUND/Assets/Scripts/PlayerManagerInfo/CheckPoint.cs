using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameCondition gameCondition;
    private List<GameObject> CheckPointKeyList;
//    private Ranking Rank;

    private void Start()
    {
        gameCondition = GameObject.FindGameObjectWithTag("ConditionManager").GetComponent<GameCondition>();
        CheckPointKeyList = gameCondition.CheckPointDictionary.Keys.ToList();
    }

    public void CountCheck(string TagName)
    {

        for (int i = 0; i < CheckPointKeyList.Count; i++)
        {
            if (CheckPointKeyList[i].ToString().StartsWith(TagName))
            {
                gameCondition.CheckPointDictionary[CheckPointKeyList[i]] = true;
            }

            if (TagName == "CheckPoint_End" && PointValueCheck())
            {
                gameCondition.TrackCountControl();
            }
        }
    }

    private bool PointValueCheck()
    {
        for (int i = 0; i < CheckPointKeyList.Count; i++)
        {
            if (gameCondition.CheckPointDictionary[CheckPointKeyList[i]] != true)
            {
                return false;
            }
        }
        return true;
    }
}

