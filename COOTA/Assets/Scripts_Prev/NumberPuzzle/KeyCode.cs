using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCode : MonoBehaviour
{
    public GameObject Txt0, Txt1, Txt2, Txt3;
    public Text text0, text1, text2, text3;
    private void Start()
    {
        Txt0 = GameObject.Find("Text0");
        Txt1 = GameObject.Find("Text1");
        Txt2 = GameObject.Find("Text2");
        Txt3 = GameObject.Find("Text3");
    }
    public void getText()
    {
        text0 = Txt0.GetComponent<Text>();
        text1 = Txt1.GetComponent<Text>();
        text2 = Txt2.GetComponent<Text>();
        text3 = Txt3.GetComponent<Text>();

        if(text0.text == "1" )
        {
            Debug.Log("맞아");
        
        }
        else
        {
            Debug.Log("아니야");
        }
   
    }
}
