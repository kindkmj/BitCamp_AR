using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    private Text text;
    private int TrackCount;
    private Text Track;
    private Text TimeCount;


    private GameObject Car;
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_handBreak;
    private float m_steeringAngle;

    private WheelCollider[] WheelColliders = new WheelCollider[4];
    //private WheelCollider frontDriverW, frontPassengerW;
    //private WheelCollider rearDriverW, rearPassengerW;
    private Transform[] WheelTransforms=new Transform[4];
    //private Transform frontDriverT, frontPassengerT;
    //private Transform rearDriverT, rearPassengerT;
    public float maxSteerAngele = 30;

    public float motorForce = 50;
   

    #region MyRegion

    private void Start()
    {
        text = GameObject.FindWithTag("Countdown").GetComponent<Text>();
        Track = GameObject.FindWithTag("Track").GetComponent<Text>();
        TrackCount = 0;
        for (int i = 0; i < 4; i++)
        {
            WheelColliders[i] = GameObject.FindGameObjectsWithTag("WheelCollider")[i].GetComponent<WheelCollider>();
        }
        for (int i = 0; i < 4; i++)
        {
            WheelTransforms[i] = GameObject.FindGameObjectsWithTag("WheelTransform")[i].GetComponent<Transform>();
        }
    }


    #endregion
    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_handBreak = Input.GetAxis("Jump");
    }
    private void Steer()
    {
       m_steeringAngle = maxSteerAngele * m_horizontalInput;
       WheelColliders[0].steerAngle = m_steeringAngle;
       WheelColliders[1].steerAngle = m_steeringAngle;
    }
    private void Accelerate()
    {
        WheelColliders[0].motorTorque = m_verticalInput * motorForce;
        WheelColliders[1].motorTorque = m_verticalInput * motorForce;
        WheelColliders[0].brakeTorque = m_handBreak * motorForce * 3;
        WheelColliders[1].brakeTorque = m_handBreak * motorForce * 3;
    }
    private void UpdateWheelPoses()
    {
        UpdatreWheelPose(WheelColliders[0], WheelTransforms[0]);
        UpdatreWheelPose(WheelColliders[1], WheelTransforms[1]);
        UpdatreWheelPose(WheelColliders[2], WheelTransforms[2]);
        UpdatreWheelPose(WheelColliders[3], WheelTransforms[3]);
    }

    private void UpdatreWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat ;
    }

    private void FixedUpdate()
    {
        
        if (text.text.Trim().Equals(""))
        {
            GetInput();
            Steer();
            Accelerate();
            UpdateWheelPoses();
            AutoMotorForce();
            TrackCheck();

        }
        //InitCarPosition();

    }
    /*
    private void InitCarPosition()
    {
        int count = 0;
        if (this.transform.rotation.x > 30 || this.transform.rotation.x < -30)
        {
            count += 1;
            Debug.Log(count);
        }
    }*/


    private void AutoMotorForce()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)))
        {
            if (motorForce < 1500)
            {
                motorForce += 10f;
            }
        }
        else
        {
            if (motorForce > 50)
                motorForce -= 5f;
        }
    }
    private void TrackCheck()
    {
        Track.text = TrackCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {Debug.Log(other.name);
        //other = Car.GetComponent<BoxCollider>();
        if (other.gameObject.CompareTag("TrackCheck"))
        {
            TrackCount++;
        }
    }
}
