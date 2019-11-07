using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLogin : MonoBehaviour
{
    public InputField IdField;
    public InputField PaField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Admin_1()
    {
        IdField.text = "123456";
        PaField.text = "123456";
    }

    public void Admin_2()
    {
        IdField.text = "11223344";
        PaField.text = "11223344";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
