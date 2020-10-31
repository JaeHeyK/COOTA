using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace prevScript
{
    public class Action_Hide : MonoBehaviour
    {
        public GameObject player;
        public Animator playerAnimator;
        public Rigidbody2D playerRigidbody2D;
        public SpriteRenderer playerRenderer;
        PlayerMove playerClass;

        private Vector3 hideWay;
        private bool isTouching = false;
        private bool isHiding = false;
        private float speed = 4f;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
            playerAnimator = player.GetComponent<Animator>();
            playerRigidbody2D = player.GetComponent<Rigidbody2D>();
            playerRenderer = player.GetComponent<SpriteRenderer>();// Player's properties

            playerClass = player.GetComponent<PlayerMove>();

        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Debug.Log("Touching");
                isTouching = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Debug.Log("Detached");
                isTouching = false;
            }

        }
        // Update is called once per frame
        void Update()
        {
            if (isTouching == true && isHiding == false && Input.GetButtonDown("Fire1"))     // Player starts hide
            {
                playerClass.interAction = true;
                if (transform.position.x > player.transform.position.x)
                    hideWay = Vector3.right;
                else
                    hideWay = Vector3.left;
                playerAnimator.SetBool("IsRun", true);
                StartCoroutine(HideInBox());
            }
            else if (isHiding == true && Input.GetButtonDown("Fire1"))                      // Player starts getout
            {
                playerAnimator.SetBool("IsRun", true);
                StartCoroutine(GetOutofBox());
            }
        }

        IEnumerator HideInBox()
        {
            float time = 0;
            while (player.transform.position.x < transform.position.x + 2.5f)
            {
                playerRenderer.flipX = true;
                yield return new WaitForSeconds(Time.deltaTime);
                player.transform.Translate(Vector3.right * speed * Time.deltaTime);
                //player.transform.localScale = Vector3.one * (1 - time); // Player being small
                time += Time.deltaTime;
            }

            while (player.transform.position.x > transform.position.x)
            {
                playerRenderer.flipX = false;
                yield return new WaitForSeconds(Time.deltaTime);
                player.transform.Translate(Vector3.left * speed * Time.deltaTime);
                //player.transform.localScale = Vector3.one * (1 - time); // Player being small
                time += Time.deltaTime;
                playerRenderer.sortingOrder = -1;
            }
            playerAnimator.SetBool("IsRun", false);
            isHiding = true;
        }

        IEnumerator GetOutofBox()
        {
            float time = 0;
            Debug.Log(player.transform.localScale.x);
            playerRenderer.flipX = true;
            while (player.transform.position.x < transform.position.x + 3.0f)
            {
                Debug.Log("커!져라~");
                yield return new WaitForSeconds(Time.deltaTime);
                player.transform.Translate(Vector3.right * speed * Time.deltaTime);
                //player.transform.localScale = Vector3.one * (time);
                time += Time.deltaTime;
            }
            playerRenderer.sortingOrder = 8;
            isHiding = false;
            playerClass.interAction = false;
        }
    }
}