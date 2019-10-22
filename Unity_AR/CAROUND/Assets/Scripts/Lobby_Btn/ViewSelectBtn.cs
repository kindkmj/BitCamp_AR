using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSelectBtn : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons= new Button[5];
    // Start is called before the first frame update
    public void ChangeColor(string text)
    {
        if (text.Trim().ToUpper().StartsWith("DE"))
        {
            ChangeColor(0);
        }
        else if (text.Trim().ToUpper().StartsWith("F1"))
        {
            ChangeColor(1);
        }
        else if (text.Trim().ToUpper().StartsWith("OF"))
        {
            ChangeColor(2);
        }
        else if (text.Trim().ToUpper().StartsWith("RA"))
        {
            ChangeColor(3);
        }
        else if (text.Trim().ToUpper().StartsWith("ST"))
        {
            ChangeColor(4);
        }
        else if (text.Trim().ToUpper().StartsWith("SU"))
        {
            ChangeColor(5);
        }
    }
    void ChangeColor(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = Color.white;
        }
        //        buttons[index].image.color = new Color(83, 227, 184);
        buttons[index].image.color=Color.cyan;
    }
}
