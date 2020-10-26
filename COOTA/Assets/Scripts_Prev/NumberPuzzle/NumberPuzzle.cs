using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPuzzle : MonoBehaviour
{
    public int count = 0;
    public Text countText;
    // Start is called before the first frame update
    void Start()
    {
        countText.text = "" + count;
    }

    // Update is called once per frame
    public void Plus()
    {
        count = count + 1;
        if(count >= 10)
        {
            count = 0;
        }
        countText.text = "" + count;
        
    }

    public Text getText()
    {
        return countText;
    }
    
}
