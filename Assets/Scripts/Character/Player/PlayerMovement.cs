using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 움직임을 관리하는 클래스
public class PlayerMovement : CharacterMovement
{
    private bool onGround = true;

    public bool OnGround { get { return onGround; } private set { this.onGround = value; } }

    protected override void Initialization()
    {
        base.Initialization();

        OnGround = true;
    }

    public void Jump() // 점프
    {
        if (!OnGround) return;

        OnGround = false;
        characterRigidbody2D.velocity += Vector2.up * fJumpPower;        
    }

    public override void Stop()
    {
        base.Stop();
        OnGround = false;
        characterRigidbody2D.velocity = Vector2.down;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGround = false;
        }
    }
}
