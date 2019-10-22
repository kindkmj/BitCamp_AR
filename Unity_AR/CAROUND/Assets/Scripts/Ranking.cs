//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.XR.WSA.Input;
//
//public class Ranking : MonoBehaviour
//{
//
//#region Variable
//
//
//    [SerializeField]
//    private GameObject[] PlayerCar; //차량
//
//    private Dictionary<GameObject,int> PlayerMovement = new Dictionary<GameObject, int>();
//    private Dictionary<GameObject, Dictionary<int,int>> PlayerMovement2 = new Dictionary<GameObject, Dictionary<int, int>>();
//
//    private int PlayerCount = 0;
//    private float MovingDistance = 0f; //이동거리
//    [SerializeField]
//    private Transform StartPosition; //시작위치
//    [SerializeField]
//    private Transform EndPosition; //시작위치
//
//
//    #endregion
//
//    #region Event
//
////    void Start()
////    {
////        PlayerMovement.Add(PlayerCar[0],PlayerCount);
////    }
////    void Update()
////    {
////        Debug.Log(Vector3.Distance(StartPosition.position, PlayerCar[0].transform.position));
////    }
//    #endregion
//
//    #region Function
//
//    private void DistanceComparison()
//    {
//        float[] a = new float[4];
//        var test = PlayerMovement.Keys.ToList();
//        for (int i = 0; i < test.Count; i++)
//        {
//            a[i] = Vector3.Distance(StartPosition.position, test[i].transform.position);
//        }
//        Array.Sort(a);
//
//    }
//    public void SetCount(string _name)
//    {
//        var test = PlayerMovement.Keys.ToList();
//        for (int i = 0; i < test.Count; i++)
//        {
//            if (test[i].name == _name)
//            {
//                PlayerMovement[test[i]] = PlayerCount+1;
//            }
//        }
//    }
//    #endregion
//}
