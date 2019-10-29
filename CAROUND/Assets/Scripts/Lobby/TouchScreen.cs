using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour 
{

    #region Variable
    public GameObject ViewRotatePart; //해당 게임오브젝트 이름을 바꾸어야 하며 게임오브젝트를 넣어주는걸 코드에서 구현 


    public  float m_speed = 0f;
    private bool turnCheck = false;
    private Vector3 Point;
    private Vector3 vec;
    #endregion

    #region Event

    void Start()
    {
        vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        Point = Camera.main.ScreenToWorldPoint(vec, Camera.MonoOrStereoscopicEye.Left);
    }

    void Update()
    {
        //안드로이드로 빌드시 위치값이 어떻게 되는지 확인해보아야함.
        if (turnCheck == true&& ViewRotatePart!=null)
        {
            ViewRotatePart.transform.Rotate(0f, Time.deltaTime * m_speed, 0f, Space.Self);
        }

    }

    #endregion

    #region Function


    #endregion

    public void TouchToMoveOn()
    {
        if (Point.x > 27)
        {
            turnCheck = true;
            m_speed = 100f;
        }
        else if (Point.x < 27)
        {
            turnCheck = true;
            m_speed = -100f;
        }
    }

    public void TOFF()
    {
        turnCheck = false;
        m_speed = 0f;

    }
  }
