using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MouseTest : MonoBehaviourPunCallbacks
{
    private RoomInformation roomInformation;
    public Text RoomB;
    private Text RoomPlayerList;
    private string roomname;

    void Start()
    {
        roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        //RoomPlayerList = GameObject.Find("RoomPlayerList").GetComponent<Text>();
        //RoomB = transform.GetChild(0).GetComponent<Text>();
    }
    public void Test()
    {
        //Dictionary<string,string> dick = new Dictionary<string, string>();

        //dick.Add("123","1234");
        //foreach (var dicklist in dick)
        //{
        //    if (dicklist.Key == "")
        //    {
        //        dick.Remove("123");
        //    }
        //}
        //List<string> test = new List<string>();
        //test = dick.Keys.ToList();
        //for (int i = 0; i < test.Count; i++)
        //{
        //    if (test[i] == "")
        //    {

        //    }
        //}
        
        roomname = RoomB.text;
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        roomInformation.SetRoomName(roomname);
        //RoomPlayerList.text = PhotonNetwork.PlayerList.ToString();
    }
}
