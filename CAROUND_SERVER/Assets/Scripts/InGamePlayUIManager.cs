using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePlayUIManager : MonoBehaviour
{
    #region Variable

    private Text CountDownText;
    private Text RankText;
    private Text[] Texts;
    private Time_AA timea;
    private GameCondition GameCondition;
    private CheckPoint checkPoint;
    private float TrackTime = 0f;
    #endregion

    void Start()
    {
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
        }
    }
    // Update is called once per frame
    void Update()
    {
        CountDown();
    }
    #region Event
    #endregion
    #region Function
    private void CountDown()
    {
        CountDownText.text = timea.CountDownTime;
        Condition();
    }
    private void Condition()
    {
        if (GameCondition.TrackCount == 3)
        {
            RankText.text = GameCondition.TrackCount.ToString();
            GameOver();
        }
        else
        {
            RankText.text = GameCondition.TrackCount.ToString();
        }
    }

    private void GameOver()
    {
        timea.CountFlag = false;
        TrackTime = float.Parse(timea.CountDownTime);
        CountDownText.text = "게임 종료!";
        Debug.Log(TrackTime);
    }
    #endregion
}
