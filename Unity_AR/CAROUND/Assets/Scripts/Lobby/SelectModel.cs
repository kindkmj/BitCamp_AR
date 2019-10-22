using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SelectModel : MonoBehaviour
{
    private Image[] thisImage;
    public GameObject gameObject;
    public Object ViewPartObject;
    public string SpriteName;
    private GameObject TouchScreenGameObject;

    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponentsInChildren<Image>();
        TouchScreenGameObject = GameObject.Find("");
    }

    public void Inis(int Index)
    {
        SpriteName=thisImage[Index].sprite.name;
        ViewPartImg();
    }

    private void ViewPartImg()
    {
        string FirstText="";
        string LastText="";
        if (SpriteName.Trim().ToUpper().StartsWith("DE"))
        {
            ViewCarParts(out FirstText, out LastText);
        }
        else if (SpriteName.Trim().ToUpper().StartsWith("F1"))
        {
            ViewCarParts(out FirstText, out LastText);
        }
        else if (SpriteName.Trim().ToUpper().StartsWith("OF"))
        {
            ViewCarParts(out FirstText, out LastText);
        }
        else if (SpriteName.Trim().ToUpper().StartsWith("RA"))
        {
            ViewCarParts(out FirstText, out LastText);
        }
        else if (SpriteName.Trim().ToUpper().StartsWith("ST"))
        {
            ViewCarParts(out FirstText, out LastText);
        }
        else if (SpriteName.Trim().ToUpper().StartsWith("SU"))
        {
            ViewCarParts(out FirstText, out LastText);
        }
    }

    private void ViewCarParts(out string FirstText, out string LastText)
    {
        FirstText = "";
        LastText = "";
        TextDivision(SpriteName, ref FirstText, ref LastText);
        InitCar(FirstText, LastText);
    }

    private void TextDivision(string ChangeText, ref string FirstText, ref string LastText)
    {
        bool Division = true;

        for (int i = 0; i < ChangeText.Length; i++)
        {
            if (Division == true)
            {
                if (ChangeText[i].ToString() != "_")
                {
                    FirstText = FirstText + ChangeText[i];
                }
                else if (ChangeText[i].ToString() == "_")
                {
                    Division = false;
                }
            }
        }
        ChangeText = ChangeText.Remove(0, FirstText.Length + 1);
        for (int i = 0; i < ChangeText.Length; i++)
        {
            LastText = LastText + ChangeText[i];
        }
    }

    string LetterDivision(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].ToString() == "_")
            {

            }
        }
        return text;
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
                        if (ViewPartObject == null)
                        {
                            ViewPartObject = Instantiate(carlist[j].Value[k], tr, Quaternion.identity);
                            //TouchScreen 에 있는 게임오브젝트에 생성되는 객체를 넣어주어야 함 
                            return;
                        }
                        else
                        {
                            Destroy(ViewPartObject);
                            ViewPartObject = Instantiate(carlist[j].Value[k], tr, Quaternion.identity);
                            return;
                        }
                    }
                }
            }
        }
    }
}