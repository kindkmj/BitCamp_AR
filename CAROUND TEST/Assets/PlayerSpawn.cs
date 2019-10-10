using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private GameObject Car;
   void Start()
   {
      Car=Resources.Load("Classic_16") as GameObject;
      GameObject.Instantiate(Car, new Vector3(15, 0, 0), Quaternion.identity);
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
