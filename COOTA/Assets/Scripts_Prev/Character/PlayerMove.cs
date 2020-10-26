using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    CapsuleCollider2D CapsuleCollider2D;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public Animator animator;

    public float movePower = 1f;
    public float jumpPower = 1f;

    public bool isJumping = false;
    public bool interAction = false;
    void Awake()
    {
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }

    void FixedUpdate()
    {
        if (interAction == false)
        {
            ColliderSize();
            Move();
            Jump();
        }
        else
            ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            interAction = false;
            animator.Rebind();
        }
    }

    void Move()
    {
        //Check Vector
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
            moveVelocity = Vector3.left;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            moveVelocity = Vector3.right;

        //Character See
        if (moveVelocity == Vector3.right)
            spriteRenderer.flipX = true;
        else if (moveVelocity == Vector3.left)
            spriteRenderer.flipX = false;
        else
            ;
        //IsWalking
        if (moveVelocity == Vector3.zero)
            animator.SetBool("IsRun", false);
        else
            animator.SetBool("IsRun", true);

        //Move
        transform.position += moveVelocity * movePower * Time.deltaTime * 5;

    }

    void Jump()
    {
        if (!isJumping)
            return;
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower * 5);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void ColliderSize()
    {
        if (animator.GetBool("IsRun") == true)
            CapsuleCollider2D.size = new Vector2(2.1f, 2.4f);
        /*
        else if (animator.GetBool("IsReady") == true)
            CapsuleCollider2D.size = new Vector2(2.4f, 2.4f);
        else if (animator.GetBool("IsPushing") == true)
            CapsuleCollider2D.size = new Vector2(2.4f, 2.4f);*/
        else
            CapsuleCollider2D.size = new Vector2(0.7f, 2.5f);
    }

}