using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolvedLightPuzzle : MonoBehaviour
{
    public GameObject Target0;
    public GameObject Target1;
    public GameObject Target2;
    public GameObject Target3;
    public GameObject Target4;

    public void check()
    {
        if(Target0.activeSelf==true && Target1.activeSelf == true && Target2.activeSelf == true && Target3.activeSelf == true)
        {
            Target4.SetActive(!Target4.active);
        }
            
    }
    
}
