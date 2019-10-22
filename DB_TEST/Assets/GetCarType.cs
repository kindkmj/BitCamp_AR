using System.Collections;
using System.Collections.Generic;
using PlayFab.DataModels;
using UnityEngine;
using UnityEngine.UI;

public class GetCarType : MonoBehaviour
{
    private PlayFabManager playfabmanager;
    private string carname;
    void Start()
    {
        playfabmanager = GameObject.Find("PlayFabManager").GetComponent<PlayFabManager>();
    }

    public void SelectCarBtn()
    {
        carname = this.name.Substring(7);
        Debug.Log(carname);
        playfabmanager.GetCarTypes(carname);
    }
}
