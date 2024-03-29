﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Object = UnityEngine.Object;

public class csTouchMgr : MonoBehaviourPun
{
    public bool Type = false;
    public Object placeObject; // 생성할 오브젝트의 프리팹
    public Text test;
    private ARRaycastManager raycastMgr;

    private GameObject map;
    // ARRaycastManager.Raycast의 결과값이 리스트 타입으로 반환하므로 
    // 아래처럼 변수를 선언하였다.

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        raycastMgr = GetComponent<ARRaycastManager>(); //스크립트 객체를 얻어옴
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            test.text = map.transform.position.ToString();
        }
        catch (Exception ex)
        {
            test.text = "아직 생성되지않음";
        }

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            // 평면으로 인식한 곳만 레이로 검출
            if (raycastMgr.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                if (map == null)
                {
                    map = (GameObject) Instantiate(placeObject, hits[0].pose.position, hits[0].pose.rotation);
                    Type = true;
                }
            }
        }
    }
}
