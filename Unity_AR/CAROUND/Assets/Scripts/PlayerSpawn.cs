using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private GameObject Player; 
    public Transform _PlayerSpawn;


    // Start is called before the first frame update
    void Awake()
    {
        Player = Resources.Load("Classic_16") as GameObject;
        _PlayerSpawn = GetComponent<Transform>();
        ViewCar();
    }
    public void ViewCar()
    {
        GameObject.Instantiate(Player, _PlayerSpawn.transform.position, _PlayerSpawn.transform.rotation);
    }
}
