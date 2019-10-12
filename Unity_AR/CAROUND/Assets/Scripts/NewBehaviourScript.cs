using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviourPunCallbacks
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
    private Transform[] WheelTransforms=new Transform[4];
    List<WheelCollider> WheelCollidersList = new List<WheelCollider>();
    List<Transform> WheelTransformsList = new List<Transform>();

    public float maxSteerAngele = 30;

    public float motorForce = 50;

    public PhotonView PV;

    #region MyRegion

    private void Start()
    {
        if (PV.IsMine)
        {
            this.gameObject.tag = "Car";
            
                InitList();
        }
        text  = GameObject.FindWithTag("Countdown").GetComponent<Text>();
        Track = GameObject.FindWithTag("Track").GetComponent<Text>();
        TrackCount = 0;
    }

    private void InitList()
    {
        for (int i = 0; i < 4; i++)
        {
            WheelCollidersList.Add(this.GetComponentsInChildren<WheelCollider>()[i]);
        }

        for (int i = 0; i < this.GetComponentsInChildren<Transform>().Length; i++)
        {
            WheelTransformsList.Add(this.GetComponentsInChildren<Transform>()[i]);
        }
            WheelColliders[0] = InitWheelColliderListValue(WheelCollidersList, "frontDriverW");
            WheelColliders[1] = InitWheelColliderListValue(WheelCollidersList, "frontPassengerW");
            WheelColliders[2] = InitWheelColliderListValue(WheelCollidersList, "rearDriverW");
            WheelColliders[3] = InitWheelColliderListValue(WheelCollidersList, "rearPassengerW");
            WheelTransforms[0] = InitTransformListValue(WheelTransformsList, "frontDriverT");
            WheelTransforms[1] = InitTransformListValue(WheelTransformsList, "frontPassengerT");
            WheelTransforms[2] = InitTransformListValue(WheelTransformsList, "rearDriverT");
            WheelTransforms[3] = InitTransformListValue(WheelTransformsList, "rearPassengerT");
    }

    #region 리스트 값 넣어주기
    /// <summary>
    /// 
    /// </summary>
    /// <param name="list"></param>
    /// <param name="Name"></param>
    /// <returns></returns>
    private WheelCollider InitWheelColliderListValue(List<WheelCollider> list,string Name)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].name.Trim() == Name.Trim())
            {
                return list[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 값을 읽어오는 순서가 불확실하여서 직접 배열의 순서를 정해주어서 불확실한 오류를 방지하기 위함
    /// </summary>
    /// <param name="list"> 리스트 값</param>
    /// <param name="Name"> 해당 오브젝트 이름</param>
    /// <returns> 같은 이름일 경우 반환되는 값</returns>
    private Transform InitTransformListValue(List<Transform> list, string Name)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].name.Trim() == Name.Trim())
            {
                return list[i];
            }
        }
        return null;
    }
#endregion 


    #endregion
    public void GetInput()
    {
        if (!photonView.IsMine)
        {
            return;
        }

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
            if (PV.IsMine)
            {

                Debug.Log(PV.ViewID);
                GetInput();
                Steer();
                Accelerate();
                UpdateWheelPoses();
                AutoMotorForce();
                TrackCheck();
            }
        }
    }


    private void AutoMotorForce()
    {
        if (Input.GetKey(KeyCode.W))

        {
            if (motorForce < 1500)
            {
                motorForce += 10f;
            }
        }
        else
        {
            motorForce = 0;
        }
    }
    private void TrackCheck()
    {
        Track.text = TrackCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TrackCheck"))
        {
            TrackCount++;
        }
    }
}
