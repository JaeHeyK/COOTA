using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject Target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetsetactive();
        }
    }
    
    public void targetsetactive()
    {
        Target.SetActive(!Target.active);
    }
}
