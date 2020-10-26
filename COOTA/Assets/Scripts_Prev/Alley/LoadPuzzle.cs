using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPuzzle : MonoBehaviour
{
    private bool isTouching = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if( collision.gameObject.name == "Player" )
        {
            Debug.Log("PuzzleButton Touched");
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isTouching = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching == true && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Puzzle On");
            GameObject.Find("Player").GetComponent<PlayerMove>().interAction = true;
            SceneManager.LoadScene("LightPuzzle", LoadSceneMode.Additive);
        }
    }
}
