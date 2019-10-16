using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_AA : MonoBehaviour
{
    #region Variable
    public string CountDownTime { get;  private set; }
    private float CountDownTimer { get; set; } = 3f;
    private bool CountFlag = false;
    private GameObject PlayerCars;
    #endregion
    void Start()
    {
        PlayerCars = GameObject.FindGameObjectWithTag("PlayerManager");
        CountDown();
    }
    // Update is called once per frame
    private void Update()
    {
        if (CountFlag == true)
        {
            TimeCheck();
        }
    }
    #region Event
    private void CountDown()
    {
        if (CountDownTimer != 0)
        {
            CountDownTimer--;
        }
        else if (CountDownTimer == 0)
        {
            CountDownTime = "게임 시작!";
        }

        if(CountDownTime == "게임 시작!")
        {
            Invoke("Test",1f);
            return;
        }
        Invoke("CountDown",1f);
        CountDownTime = CountDownTimer.ToString();
    }

    private void Test()
    {
            PlayerCars.GetComponent<PlayController>().Test = true;
        CountFlag = true;
    }

    private void TimeCheck()
    {
        try
        {
        CountDownTimer += Time.deltaTime;
            CountDownTime = CountDownTimer.ToString();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion
    #region Function

    #endregion
    // Start is called before the first frame update
}
