using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject Car;
    public Text Countdown;
    private Text TimeCount;
    private Vector3 moveDirection;
    private bool Startcondition=false;
    private bool abc = true;
    private float i = 0;
    private int min = 0;



    private float Countdownnum;

    // Start is called before the first frame update
    void Start()
    {
        Car = GameObject.FindWithTag("Car");
        TimeCount = GameObject.FindWithTag("Time").GetComponent<Text>();
        Countdownnum = 4;
        Countdown.text =Countdownnum.ToString();
        countdown();
        Invoke("initText", 4f);
    }

    public bool GetCondition()
    {
        return Startcondition;
    }

    void Update()
    {
        if (Startcondition == true)
        {

        }

        if (abc == false)
        {
            
            i += Time.deltaTime;
                TimeCount.text = min +" . "+ string.Format("{00:N2}", i);
            if (i > 60)
            {
                min++;
                i = i - 60;
            }

            //Countdown.text.Remove()
        }
    }

    void initText()
    {
        Countdown.text = string.Empty;
        abc = false;
    }

    void countdown()
    {
        if (Countdownnum > 1)
        {
            Countdownnum -= 1;
            Countdown.text = string.Format("{0:N0}", Countdownnum);
            //Countdown.text = Countdownnum.ToString();

        }
        else
        {
            Startcondition = true;
            Countdown.text = "Start";
        }

        if (Countdown.text.Trim() != "Start")
        {
            Invoke("countdown", 1f);
        }
    }

    #region 전역변수

    #endregion
    #region 이벤트


    #endregion

    #region 함수
    void MoveCar()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        hor = hor * 30 * Time.deltaTime;
        ver = ver * 10 * Time.deltaTime;
        Car.transform.Rotate(Vector3.up * hor);
        Car.transform.Translate(0f, 0f, ver);
    }


    #endregion

    

    

}
