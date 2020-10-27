using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace prevScript
{
    public class EndPuzzle : MonoBehaviour
    {
        private bool isTouching = false;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
                isTouching = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
                isTouching = false;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isTouching == true && Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Alley", LoadSceneMode.Single);

            }

        }
    }
}
