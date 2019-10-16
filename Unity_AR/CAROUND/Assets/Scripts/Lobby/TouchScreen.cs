using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour
{

    #region Variable

    public GameObject gameObject;
  

          public  float m_speed = 0f;
    #endregion

    #region Event

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            m_speed = 100f;
            gameObject.transform.Rotate(0f,Time.deltaTime*m_speed,0f,Space.Self);
        }
        else
        {
            m_speed = 0f;
        }
    }

    #endregion

    #region Function
  

    #endregion
}
