using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Action_Hide: MonoBehaviour
{
    public GameObject player;
    public Animator playerAnimator;
    public Rigidbody2D playerRigidbody2D;
    public BoxCollider2D playerBoxCollider2D;
    public SpriteRenderer playerRenderer;

    private Vector3 hideWay = Vector3.right;
    private bool isTouching = false;
    private bool isHiding = false;
    private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        playerRenderer = player.GetComponent<SpriteRenderer>();// Player's properties

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
            playerRenderer.sortingOrder = -1;
            playerAnimator.SetBool("IsWalking", true);
            StartCoroutine(HideInBox());
        }
        else if (isHiding == true && Input.GetButtonDown("Fire1"))                      // Player starts getout
        {
            playerAnimator.SetBool("IsWalking", true);
            StartCoroutine(GetOutofBox());
        }
    }

    IEnumerator HideInBox()
    {
        float time = 0;
        while (player.transform.position.x > transform.position.x + 0.25f ||
            player.transform.position.x < transform.position.x - 0.25f )
        {
            Debug.Log("Box:    " + transform.position.x);
            Debug.Log("Player: " + player.transform.position.x);
            yield return new WaitForSeconds(Time.deltaTime);
            player.transform.Translate(hideWay * speed * Time.deltaTime);
            //player.transform.localScale = Vector3.one * (1 - time); // Player being small
            time += Time.deltaTime;                                 
        }
        isHiding = true;
    }

    IEnumerator GetOutofBox()
    {
        float time = 0;
        Debug.Log(player.transform.localScale.x);
        if (hideWay == Vector3.right)
            playerRenderer.flipX = false;
        else
            playerRenderer.flipX = true;
        while (player.transform.position.x < transform.position.x + 1.75f &&
            player.transform.position.x > transform.position.x - 1.75f)
        {
            Debug.Log("커!져라~");
            yield return new WaitForSeconds(Time.deltaTime);
            player.transform.Translate(-hideWay * speed * Time.deltaTime);
            //player.transform.localScale = Vector3.one * (time);
            time += Time.deltaTime;
        }
        player.transform.localScale = Vector3.one;
        playerRenderer.sortingOrder = 1;
        isHiding = false;
    }
}
