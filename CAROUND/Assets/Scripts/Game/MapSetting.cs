using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour
{
    public GameObject Map;
    public GameObject GameUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MapScalePlus()
    {
        Map.transform.localScale += new Vector3(0.1f,0.1f,0.1f);
    }

    public void MapScaleSub()
    {
        Map.transform.localScale -= new Vector3(0.1f,0.1f,0.1f);
    }

    public void MapSetOK()
    {
        this.gameObject.SetActive(false);
        GameUI.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
