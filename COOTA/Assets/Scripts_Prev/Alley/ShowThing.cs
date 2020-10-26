using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowThing : MonoBehaviour
{
    public bool isTouching = false;
    GameObject ctrl1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isTouching = true;
            Debug.Log("Show!!");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
        Debug.Log("Not showing");
    }

    // Start is called before the first frame update
    void Start()
    {
        ctrl1 = GameObject.Find("Ctrl1");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching == true)
            ctrl1.SetActive(true);
        else
            ctrl1.SetActive(false);
    }
}