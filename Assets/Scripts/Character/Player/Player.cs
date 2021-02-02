using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 스펙을 명시하는 클래스
public class Player : Character
{
    protected enum State { none, hide, climb };

    [SerializeField] protected float fJumpPower = 5f;
    [SerializeField] protected float fClimbSpeed = 3f;

    [SerializeField] protected bool isHiding;
    [SerializeField] protected bool isClimbing;
    [SerializeField] protected bool onGround;

    public bool IsHiding { get { return isHiding; } protected set { this.isHiding = value; } }
    public bool IsClimbing { get { return isClimbing; } protected set { this.isClimbing = value; } }
    public bool OnGround { get { return onGround; } protected set { this.onGround = value; } }
    public override bool CanMove { get { return base.CanMove && !IsHiding; } protected set { base.CanMove = value; } }
    public bool CanClimb { get; set; }
    public bool OnTopLadder { get; set; }
    public bool OnBotLadder { get; set; }

    protected override void Initialization()
    {
        base.Initialization();

        IsHiding = false;
        IsClimbing = false;
        CanClimb = false;
        OnTopLadder = false;
        OnBotLadder = false;
        OnGround = true;
    }

    public override void Move(Vector2 movementInput)
    {
        base.Move(movementInput);

        Climb(movementInput);
    }

    public void Jump() // 점프
    {
        if (!OnGround || IsClimbing) return;

        OnGround = false;
        groundType = GroundType.None;
        characterRigidbody2D.velocity += Vector2.up * fJumpPower;        
    }

    public void Climb(Vector2 climbInput)  // 사다리 오르내리기
    {
        if (!CanClimb)
        {
            IsClimbing = false;
            return;
        }

        if (OnTopLadder)
        {
            if (climbInput.y.CompareTo(0f) < 0)
            {
                isClimbing = true;
            }
            else if (climbInput.y.Equals(0f))
            {
                characterRigidbody2D.velocity = new Vector2(climbInput.x, 0f);
            }
        }
        else if (OnBotLadder)
        {
            if (climbInput.y.CompareTo(0f) < 0)
            {
                isClimbing = false;
            }
            else if (climbInput.y.CompareTo(0f) > 0)
            {
                isClimbing = true;
            }
            else
            {
                characterRigidbody2D.velocity = new Vector2(climbInput.x, 0f);
            }
        }

        if (!IsClimbing) return;

        characterRigidbody2D.velocity = climbInput * fClimbSpeed;
        
        characterAnimator.SetFloat(animatorMoveSpeed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGround = true;
            groundType = GroundType.Dirt;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGround = false;
            groundType = GroundType.None;
        }
    }
    public void OnLadder(bool bOnLadder)
    {
        characterCollider2D.isTrigger = bOnLadder;
    }
}
