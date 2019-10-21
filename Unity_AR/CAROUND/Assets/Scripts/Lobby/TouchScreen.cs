using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour ,IPointerClickHandler
{

    #region Variable
    public GameObject gameObject; //해당 게임오브젝트 이름을 바꾸어야 하며 게임오브젝트를 넣어주는걸 코드에서 구현 


    public  float m_speed = 0f;
    private bool turnCheck = false;
    private Vector3 Point;
    #endregion

    #region Event

    void Start()
    {

    }

    void Update()
    {
        Vector3 vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        Point = Camera.main.ScreenToWorldPoint(vec, Camera.MonoOrStereoscopicEye.Left);
        //Debug.Log(Point);
        //point의 위치가 가변적이므로 확실하지 않은 방법입니다.

        //        if (Input.GetMouseButton(0))
        //        {

        if (turnCheck == true)
        {
            gameObject.transform.Rotate(0f, Time.deltaTime * m_speed, 0f, Space.Self);
        }

//        }
//        else if (Input.GetMouseButton(1))
//        {
//            m_speed = -100f;
//            gameObject.transform.Rotate(0f, Time.deltaTime * m_speed, 0f, Space.Self);
//        }
//        else
//        {
//            m_speed = 0f;
//        }
    }

    #endregion

    #region Function


    #endregion

    public void TON()
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
  
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(11);
    }
}
