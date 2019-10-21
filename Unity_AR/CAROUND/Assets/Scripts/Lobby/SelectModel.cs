using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SelectModel : MonoBehaviour
{
    private Image thisImage;
    public GameObject gameObject;

    public string SpriteName;

    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponent<Image>();
    }

    public void DebugName()
    {
        Debug.Log(this.thisImage.sprite.name);
    }

    public void Inis()
    {
        Debug.Log(this.thisImage.sprite.name.ToUpper());
        SpriteName = thisImage.sprite.name;
        if (SpriteName.Trim().ToUpper().StartsWith("DE"))
        {
            InitCar(SpriteName.Remove(9),SpriteName.Remove(0, 10));
        }
//        else if (SpriteName.Trim().ToUpper().StartsWith("F1"))
//        {
//            SpriteName = SpriteName.Remove(0, 7);
//            InitCar(SpriteName);
//        }
//        else if (SpriteName.Trim().ToUpper().StartsWith("OF"))
//        {
//            SpriteName = SpriteName.Remove(0, 13);
//            InitCar(SpriteName);
//        }
//        else if (SpriteName.Trim().ToUpper().StartsWith("RA"))
//        {
//            SpriteName = SpriteName.Remove(0, 12);
//            InitCar(SpriteName);
//        }
//        else if (SpriteName.Trim().ToUpper().StartsWith("ST"))
//        {
//            SpriteName = SpriteName.Remove(0, 10);
//            InitCar(SpriteName);
//        }
//        else if (SpriteName.Trim().ToUpper().StartsWith("SU"))
//        {
//            SpriteName = SpriteName.Remove(0, 13);
//            InitCar(SpriteName);
//        }

        //key로 대분류를 찾은뒤 value가 name과 같은 놈을 소환시켜야함.
    }

    void InitCar(string _SpriteName,string _Name)
    {
        Vector3 tr;
        tr = new Vector3(-26.33f, -9.89f, 0.89f);
        int i = 0;

        var carlist = gameObject.gameObject.GetComponent<ViewCarsPart>().CarResources.ToList();

        for (int j = 0; j < carlist.Count; j++)
        {
            if (carlist[j].Key == _SpriteName)
            {
                for (int k = 0; k < carlist[i].Value.Length; k++)
                {
                    if (carlist[j].Value[k].name == _Name)
                    {
                        Instantiate(carlist[j].Value[k], tr, Quaternion.identity);
                        //TouchScreen 에 있는 게임오브젝트에 생성되는 객체를 넣어주어야 함 
                        return;
                    }
                }
            }
        }
    }
}