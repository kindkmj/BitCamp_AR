﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Time_AA : MonoBehaviourPunCallbacks
{
    //15초의 대기시간을 주는 이유는 초기화셋팅시 필요한 시간이라고 추측함
    #region Variable
    public string CountDownTime { get;  private set; }
    private float CountDownTimer { get; set; } = 2f;
    public bool CountFlag = false;
    private GameObject PlayerCars;
    public MapSetting map;
    private PhotonView pv;
    #endregion
    void Start()
    {
        //pv = GetComponent<PhotonView>();
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
            map.MapSetOK();
        }

        if(CountDownTime == "게임 시작!")
        {
            Invoke("Ready",1f);
            return;
        }
        Invoke("CountDown",1f);
        CountDownTime = CountDownTimer.ToString();
    }

    private void Ready()
    {
            PlayerCars.GetComponent<PlayController>().Ready = true;
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
