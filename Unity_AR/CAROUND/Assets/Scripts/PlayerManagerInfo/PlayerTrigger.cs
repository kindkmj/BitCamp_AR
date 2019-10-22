using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTrigger : MonoBehaviour
{
    private CheckPoint checkPoint;
    private bool CheckActiveScene = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Trim() == "GameScene")
        {
            checkPoint = GameObject.Find("CheckPoint").GetComponent<CheckPoint>();
            CheckActiveScene = true;
        }
        else if (SceneManager.GetActiveScene().name.Trim() != "GameScene")
        {
            CheckActiveScene = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CheckActiveScene == true)
        {
            if (other.name.StartsWith("Ch"))
            {
                checkPoint.CountCheck(other.name);
            }
        }
    }
}
