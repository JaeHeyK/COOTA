using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    None,
    Dirt,
}

// 캐릭터 움직임을 관리하는 클래스
public class CharacterMovement : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] protected Transform trPuppet = null;              // 캐릭터 본체
    [SerializeField] protected CharacterAudio characterAudio = null;   // 캐릭터 사운드

    [Header("Effect")]
    [SerializeField] protected ParticleSystem moveDust;

    [Header("Component")]
    [SerializeField] protected Animator characterAnimator = null;
    [SerializeField] protected Rigidbody2D characterRigidbody2D = null;

    [Header("Status")]
    [SerializeField] protected GroundType groundType;

    [Header("Movement")]
    [SerializeField] protected float fAccel = 30f;
    [SerializeField] protected float fMaxSpeed = 4f;
    [SerializeField] protected float minFlipSpeed = 0.1f;
    [SerializeField] protected float fJumpPower = 5f;

    private int animatorMoveSpeed;
    private bool canMove;

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        characterRigidbody2D = GetComponent<Rigidbody2D>();

        animatorMoveSpeed = Animator.StringToHash("MoveSpeed");
        canMove = true;
    }

    public virtual void Move(Vector2 movementInput) // 좌우 이동
    {
        if (!canMove) return;

        // 이동속도 설정
        Vector2 velocity = characterRigidbody2D.velocity;

        velocity += movementInput * fAccel * Time.fixedDeltaTime;

        // 이동속도 최대치 설정
        velocity.x = Mathf.Clamp(velocity.x, -fMaxSpeed, fMaxSpeed);

        characterRigidbody2D.velocity = velocity;

        SetDirection();

        // 애니메이터 재생 속도 설정
        float fNormalizedMoveSpeed = Mathf.Abs(velocity.x) / fMaxSpeed;

        characterAnimator.SetFloat(animatorMoveSpeed, fNormalizedMoveSpeed);
        
        characterAudio?.PlaySteps(groundType, fNormalizedMoveSpeed);
    }
    protected virtual void SetDirection() // 캐릭터 좌우 반전 설정
    {
        // 오른쪽 이동 후 정지 시 오른쪽을 바라보게 왼쪽 이동 후 정지 시 왼쪽을 바라보게 설정
        if (characterRigidbody2D.velocity.x > minFlipSpeed)
        {
            trPuppet.localScale = Vector2.one;
        }
        else if (characterRigidbody2D.velocity.x < -minFlipSpeed)
        {
            trPuppet.localScale = Global.flippedScale;
        }
    }

    public virtual void Stop() // 정지
    {
        characterRigidbody2D.velocity = Vector2.zero;
        Move(Vector2.zero);
    }
}
