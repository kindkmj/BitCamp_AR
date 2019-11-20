using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTrigger : MonoBehaviour
{
    private CheckPoint checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        checkPoint = GameObject.Find("CheckPoint").GetComponent<CheckPoint>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Ch"))
        {
            checkPoint.CountCheck(other.name);
        }
    }
}
