using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelegateTest : MonoBehaviour
{
    private Button btn;
    private GameObject room;

    // Start is called before the first frame update
    void Start()
    {
        room = GameObject.Find("RoomManager");
    }

    public void test()
    {
        btn.GetComponent<Button>().onClick.AddListener(delegate() {room.GetComponent<RoomInformation>().CreateRoom();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
