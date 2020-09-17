using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    Transform AT;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        AT = player.transform;

        transform.position = new Vector3(AT.position.x, AT.position.y + 1.75f , transform.position.z);
    }
}
