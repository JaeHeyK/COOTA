using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임을 플레이하는 플레이어는 유일하기에 Singleton으로 보장
// 모든 동작은 PlayerController 클래스를 통해 되도록 구조 설계
public class Player : Singleton<Player>
{
    [Header("Character")]
    [SerializeField] private Transform trPuppet = null;       // 캐릭터 본체

    [Header("Component")]
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private Rigidbody2D playerRigidbody2D = null;

    [Header("Movement")]
    [SerializeField] private float fAccel = 30f;
    [SerializeField] private float fMaxSpeed = 4f;
    [SerializeField] private float minFlipSpeed = 0.1f;
    [SerializeField] private float fJumpPower = 5f;

    private int animatorMoveSpeed;

    private void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        animatorMoveSpeed = Animator.StringToHash("MoveSpeed");
    }

    public void Move(Vector2 moveInput) // 좌우 이동
    {
        // 이동속도 설정
        Vector2 velocity = playerRigidbody2D.velocity;

        velocity += moveInput * fAccel * Time.fixedDeltaTime;
        velocity.x = Mathf.Clamp(velocity.x, -fMaxSpeed, fMaxSpeed);
        playerRigidbody2D.velocity = velocity;

        SetDirection();

        // 애니메이터 이동 패러미터 설정
        float fNormalizedMoveSpeed = Mathf.Abs(velocity.x) / fMaxSpeed;

        playerAnimator.SetFloat(animatorMoveSpeed, fNormalizedMoveSpeed);        
    }
    private void SetDirection() // 캐릭터 좌우 반전 설정
    {
        // 오른쪽 이동 후 정지 시 오른쪽을 바라보게 왼쪽 이동 후 정지 시 왼쪽을 바라보게 설정
        if (playerRigidbody2D.velocity.x > minFlipSpeed)
        {
            trPuppet.localScale = Vector2.one;
        }
        else if (playerRigidbody2D.velocity.x < -minFlipSpeed)
        {
            trPuppet.localScale = Global.flippedScale;
        }
    }

    public void Jump() // 점프
    {
        playerRigidbody2D.velocity += Vector2.up * fJumpPower;        
    }
    public void Stop() // 정지
    {
        playerRigidbody2D.velocity = Vector2.zero;
        Move(Vector2.zero);
    }
}
