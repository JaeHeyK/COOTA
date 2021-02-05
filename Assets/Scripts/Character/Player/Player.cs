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
    [SerializeField] protected bool isJumping;
    [SerializeField] protected bool canClimb;
    [SerializeField] protected bool onTopLadder;
    [SerializeField] protected bool onBotLadder;

    public bool IsHiding { get { return isHiding; } protected set { this.isHiding = value; } }
    public bool IsClimbing { get { return isClimbing; } protected set { this.isClimbing = value; } }
    public bool IsJumping { get { return isJumping; } protected set { this.isJumping = value; } }
    public override bool CanMove { get { return base.CanMove && !IsHiding; } protected set { base.CanMove = value; } }
    public bool CanClimb { get { return canClimb; } set { this.canClimb = value; } }
    public bool OnTopLadder { get { return onTopLadder; } set { this.onTopLadder = value; } }
    public bool OnBotLadder { get { return onBotLadder; } set { this.onBotLadder = value; } }

    protected override void Initialization()
    {
        base.Initialization();

        IsHiding = false;
        IsClimbing = false;
        CanClimb = false;
        OnTopLadder = false;
        OnBotLadder = false;
        IsJumping = false;
    }

    public override void Move(Vector2 movementInput)
    {
        base.Move(movementInput);

        Climb(movementInput);
    }

    public void Jump() // 점프
    {
        if (IsJumping || IsClimbing) return;

        IsJumping = true;
        groundType = GroundType.None;
        characterRigidbody2D.AddForce(Vector2.up * fJumpPower, ForceMode2D.Impulse);
        characterRigidbody2D.gravityScale = 1f;
    }

    public void Climb(Vector2 climbInput)  // 사다리 오르내리기
    {
        // 오르내릴 수 있는지 확인
        if (!CanClimb)
        {
            IsClimbing = false;
            characterRigidbody2D.gravityScale = 1f;
            characterCollider2D.isTrigger = false;
            return;
        }
        
        // 사다리 윗 파트
        if (OnTopLadder)
        {
            // 위층 -> 아래층
            if (climbInput.y.CompareTo(0f) < 0)
            {
                IsClimbing = true;
                characterCollider2D.isTrigger = true;
            }
        }
        // 사다리 아래 파트
        else if (OnBotLadder)
        {
            if (climbInput.y.CompareTo(0f) < 0)
            {   
                characterCollider2D.isTrigger = false;
            }
            else if (climbInput.y.CompareTo(0f) > 0)
            {
                IsClimbing = true;
                characterCollider2D.isTrigger = true;
            }
        }
        // 사다리 가운데 파트
        else if (CanClimb)
        {
            if (!climbInput.y.Equals(0f))
            {
                IsClimbing = true;
                characterCollider2D.isTrigger = true;
            }
        }

        if (!IsClimbing) return;

        characterRigidbody2D.gravityScale = 0f;
        characterRigidbody2D.velocity = new Vector2(characterRigidbody2D.velocity.x, climbInput.y * fClimbSpeed);
    }

    public void EnterGround(GroundType type)
    {
        if (!IsClimbing)
        {
            IsJumping = false;
            groundType = type;
        }
    }
    public void ExitGround()
    {
        groundType = GroundType.None;
    }
}
