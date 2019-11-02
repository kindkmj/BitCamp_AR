using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayController : MonoBehaviour
{
    #region variable

    private PhotonView pv;
    private List<string> WheelList = new List<string>();
    private List<string> TireList = new List<string>();
    public float speed;
    private Joystick VariableJoystick;
    private WheelCollider[] PlayerWheels = new WheelCollider[4];
    private Transform[] PlayerTransform = new Transform[4];
    private int WheelAndTireTransformCount = 4;
    private float m_steeringAngle;
    private Vector3 passPosition;
    [SerializeField]
    private float motorForce = 1000f;
    private GameObject MyCar;
    private Rigidbody rigidbody;
    public bool Ready = false;
    public RoomInformation _RoomInformation;
    #endregion
    #region Event

    public void FixedUpdate()
    {
        if (Ready == true)
        {
            Debug.Log(MyCar.name);
        }

        if (Ready == true)
        {
            Debug.Log(MyCar.name);
            MoveCar();
        }
    }

    void Start()
    {
            InitSetUp();
    }

    private void InitSetUp()
    {
            if (Ready == true)
            {
                {
                    _RoomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
                    VariableJoystick = GameObject.FindWithTag("JoyStick").GetComponent<Joystick>();
                    WheelCollider[] DefalutWheelColliders = null;
                    Transform[] DefalutTranforms = null;
                    MyCar = GameObject.Find(_RoomInformation.MyName);
                    InitWheelAndTireName();
                    InitComponents("W", WheelList, MyCar, ref PlayerWheels, ref DefalutTranforms);
                    InitComponents("T", TireList, MyCar, ref DefalutWheelColliders, ref PlayerTransform);
                    rigidbody = MyCar.GetComponent<Rigidbody>();
                    rigidbody.centerOfMass = new Vector3(0.0f, 0.15f, -0.1f);
                }
            }
            else
            {
                Invoke("InitSetUp", 1f);
            }
    }

    #endregion
    #region Function
    /// <summary>
    /// 휠과 트랜스폼의 리스트에 이름을 입력해준뒤 해당 이름과 같은지 다른지를 비교하기 위함.
    /// </summary>
    private void InitWheelAndTireName()
    {
        WheelList.Add("FrontWheelL");
        WheelList.Add("FrontWheelR");
        WheelList.Add("BackWheelL");
        WheelList.Add("BackWheelR");
        TireList.Add("FrontTireL");
        TireList.Add("FrontTireR");
        TireList.Add("BackTireL");
        TireList.Add("BackTireR");
    }
    /// <summary>
    /// 휠이나 트랜스폼의 초기화를 위한 함수
    /// </summary>
    /// <param name="ProcCase">ProcCase 가 W인 경우에는 휠 콜라이더 T인 경우에는 트랜스폼 을 초기화</param>
    /// <param name="ListName">'WheelList', 'TireList'중 택1 하여 넣어줌 </param>
    private void InitComponents(string ProcCase,List<string> ListName, GameObject gameObject, ref WheelCollider[] wheelColliders,ref Transform[] transforms)
    {
        if (ProcCase == "W")
        {
            wheelColliders = gameObject.GetComponentsInChildren<WheelCollider>();
        }
        else if (ProcCase == "T")
        {
            Transform[] transfromList;
            int Count = 0;
            transfromList = gameObject.GetComponentsInChildren<Transform>();
            for (int i = 0; i < transfromList.Length; i++)
            {
                for (int j = 0; j < ListName.Count; j++)
                {
                    if (transfromList[i].name.Trim() == ListName[j].Trim())
                    {
                        transforms[Count] = transfromList[i];
                        Count++;
                    }
                }                                                                                                                                                                                                     
            }
        }
    }

    public void MoveCar()
    {
        float m_horizontalInput = VariableJoystick.Horizontal;
        float m_verticalInput = VariableJoystick.Vertical;
        float m_handBreak = Input.GetAxis("Jump");
        PlayerWheels[0].motorTorque = m_verticalInput * motorForce*Time.deltaTime;
        PlayerWheels[1].motorTorque = m_verticalInput * motorForce*Time.deltaTime;
        PlayerWheels[0].brakeTorque = m_handBreak * motorForce * 3*Time.deltaTime;
        PlayerWheels[1].brakeTorque = m_handBreak * motorForce * 3*Time.deltaTime;
        m_steeringAngle = 30 * m_horizontalInput;
        PlayerWheels[0].steerAngle = m_steeringAngle;
        PlayerWheels[1].steerAngle = m_steeringAngle;
        UpdateWheelPoses();
        Debug.Log(m_horizontalInput.ToString());
    }

    private void UpdateWheelPoses()
    {
        UpdatreWheelPose(PlayerTransform[0],PlayerWheels[0]);
        UpdatreWheelPose(PlayerTransform[1],PlayerWheels[1]);
        UpdatreWheelPose(PlayerTransform[2],PlayerWheels[2]);
        UpdatreWheelPose(PlayerTransform[3],PlayerWheels[3]);
    }

    private void UpdatreWheelPose(Transform _transform,WheelCollider _collider)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    #endregion
}