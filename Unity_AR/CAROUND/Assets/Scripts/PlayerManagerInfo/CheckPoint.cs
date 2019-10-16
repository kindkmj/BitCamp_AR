using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameCondition gameCondition;

    private void Start()
    {
        gameCondition = GameObject.FindGameObjectWithTag("ConditionManager").GetComponent<GameCondition>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckTransform")
        {
            for (int i = 0; i < gameCondition.GetComponent<GameCondition>().CheckPointCondition.Length; i++)
            {
                if (gameCondition.GetComponent<GameCondition>().CheckPointCondition[i] != true)
                {
                    gameCondition.GetComponent<GameCondition>().CheckPointCondition[i] = true;
                    return;
                }
            }
        }
    }
}
