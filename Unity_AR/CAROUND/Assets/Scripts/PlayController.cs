using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    private WheelCollider[] PlayerWheels = new WheelCollider[4];
    private Transform[] PlayerTransform = new Transform[4];
    private int WheelAndTireTransformCount = 4;
    private float m_steeringAngle;
    private Vector3 passPosition;
    float motorForce =100f;
    void Start()
    {
        for (int i = 0; i < WheelAndTireTransformCount; i++)
        {
            PlayerTransform[i] = GameObject.FindGameObjectsWithTag("Tire")[i].GetComponent<Transform>();
            PlayerWheels[i] = GameObject.FindGameObjectsWithTag("Wheel")[i].GetComponent<WheelCollider>();
        }
    }

    public void MoveCar()
    {
        float m_horizontalInput = Input.GetAxis("Horizontal");
        float m_verticalInput = Input.GetAxis("Vertical");
        float m_handBreak = Input.GetAxis("Jump");

//        if (Input.GetKey(KeyCode.W))
//        {
//            
//            motorForce += 50;
//            Debug.Log("속도"+motorForce + " rpm= "+PlayerWheels[0].rpm+ " 모터토크 = " +PlayerWheels[0].motorTorque);
//        }
//        else
//        {
//            if(motorForce>50)
//            motorForce -= 50;
//        }
        
        PlayerWheels[0].motorTorque = m_verticalInput * motorForce;
        PlayerWheels[1].motorTorque = m_verticalInput * motorForce;
        PlayerWheels[0].brakeTorque = m_handBreak * motorForce * 3;
        PlayerWheels[1].brakeTorque = m_handBreak * motorForce * 3;

        m_steeringAngle = 30 * m_horizontalInput;
        PlayerWheels[0].steerAngle = m_steeringAngle;
        PlayerWheels[1].steerAngle = m_steeringAngle;
        UpdateWheelPoses();

    }

    private void UpdateWheelPoses()
    {
        UpdatreWheelPose(PlayerWheels[0], PlayerTransform[0]);
        UpdatreWheelPose(PlayerWheels[1], PlayerTransform[1]);
        UpdatreWheelPose(PlayerWheels[2], PlayerTransform[2]);
        UpdatreWheelPose(PlayerWheels[3], PlayerTransform[3]);
    }
  
    private void UpdatreWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);


        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    void Update()
    {
        MoveCar();
    }
}
