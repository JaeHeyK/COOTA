using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 스펙을 명시하는 클래스
public class Player : Character
{
    [SerializeField] protected float fJumpPower = 5f;

    [SerializeField] protected bool isHiding = false;
    [SerializeField] protected bool onGround = true;

    public bool IsHiding { get { return isHiding; } protected set { this.isHiding = value; } }
    public bool OnGround { get { return onGround; } protected set { this.onGround = value; } }
    public override bool CanMove { get { return base.CanMove && !IsHiding; } protected set { base.CanMove = value; } }

    protected override void Initialization()
    {
        base.Initialization();

        OnGround = true;
        IsHiding = false;
    }

    public void Jump() // 점프
    {
        if (!OnGround) return;

        OnGround = false;
        groundType = GroundType.None;
        characterRigidbody2D.velocity += Vector2.up * fJumpPower;        
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
}
