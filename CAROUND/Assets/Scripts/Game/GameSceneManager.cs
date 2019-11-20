using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameSceneManager : MonoBehaviourPunCallbacks
{
    public GameObject GameUI;

    public GameObject RewardUI;

    public GameObject MapSetting;

    // Start is called before the first frame update
    void Awake()
    {
        GameUI.SetActive(false);
        RewardUI.SetActive(false);
        MapSetting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
