using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnOff : InitRoomScene
{
    //private GameObject Login;
    //private GameObject Lobby;
    //private GameObject RoomCreate;
    //private GameObject Room;
    //private GameObject RoomInside;
    //private GameObject AppStart;
    //private GameObject Register;
    //private GameObject FindID;
    //private GameObject FindPassWord;

    public RoomInformation room;
    private GameObject[] Panels;
    // Start is called before the first frame update
    void Start()
    {
        //Login = GameObject.FindWithTag("Login");
        //Lobby = GameObject.FindWithTag("Lobby");
        //RoomCreate = GameObject.FindWithTag("RoomCreate");
        //Room = GameObject.FindWithTag("Room");
        //RoomInside = GameObject.FindWithTag("RoomInside");
        //AppStart = GameObject.FindWithTag("AppStart");
        //Register = GameObject.FindWithTag("Register");
        //FindID = GameObject.FindWithTag("FindID");
        //FindPassWord = GameObject.FindWithTag("FindPassWord");

        //Panels[0] = Login;
        if (room.MoveScene == false)
        {
            Panels = GameObject.FindGameObjectsWithTag("Panel");
            PanelOn("AppStart");
        }
    }

    public void PanelOn(string PanelName)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i].name == PanelName)
            {
                Panels[i].SetActive(true);
            }
            else
            {
                Panels[i].SetActive(false);
            }
        }
    }
}
