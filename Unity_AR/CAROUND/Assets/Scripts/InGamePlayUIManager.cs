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
    #endregion

    void Start()
    {
        GameCondition = GameObject.FindGameObjectWithTag("ConditionManager").GetComponent<GameCondition>();
        GameCondition.InitCheckPointCondition();

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
        Condition();
    }

    #region Event


    #endregion
    #region Function
    private void CountDown()
    {
        CountDownText.text = timea.CountDownTime;
    }
    private void Condition()
    {
        for (int i = 0; i < GameCondition.CheckPointCondition.Length; i++)
        {
            Debug.Log(i);
            if (GameCondition.CheckPointCondition[i] != true)
            {
                CountDown();
                RankText.text = $"{GameCondition.TrackCount.ToString()} / 3 ";
                return;
            }
            else if(GameCondition.TrackCount >= 3) GameOver();
            else if (GameCondition.TrackCount < 3 && GameCondition.CheckPointCondition[GameCondition.CheckPointCondition.Length-1]==true)
            {
                GameCondition.InitCheckPointCondition();
            }
        }
    }

    private void GameOver()
    {
        CountDownText.text = "게임 종료!";
    }
    #endregion
}
