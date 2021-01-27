using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    None,
    Dirt,
}

// 캐릭터 스펙을 명시하는 클래스
public class Character : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] protected Transform trPuppet = null;              // 캐릭터 본체
    [SerializeField] protected CharacterAudio characterAudio = null;   // 캐릭터 사운드

    [Header("Effect")]
    [SerializeField] protected CharacterEffect characterEffect = null;

    [Header("Component")]
    [SerializeField] protected Animator characterAnimator = null;
    [SerializeField] protected Rigidbody2D characterRigidbody2D = null;

    [Header("Status")]
    [SerializeField] protected GroundType groundType;
    [SerializeField] protected bool isAlive;

    [Header("Movement")]
    [SerializeField] protected float fAccel = 30f;
    [SerializeField] protected float fMaxSpeed = 4f;
    [SerializeField] protected float fMinFlipSpeed = 0.1f;    

    protected int animatorMoveSpeed;
    protected bool canMove;
    
    public bool IsAlive { get { return isAlive; } protected set { isAlive = value; } }
    public bool CanMove { get { return canMove; } protected set { canMove = value; } }

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        characterAnimator = GetComponent<Animator>();
        characterRigidbody2D = GetComponent<Rigidbody2D>();

        animatorMoveSpeed = Animator.StringToHash("MoveSpeed");

        IsAlive = true;
        CanMove = true;
        groundType = GroundType.Dirt;

        characterAnimator.SetFloat(animatorMoveSpeed, 0f);
    }

    public virtual void Move(Vector2 movementInput) // 좌우 이동
    {
        if (!CanMove) return;

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
        characterEffect?.PlayEffects(groundType, fNormalizedMoveSpeed);
    }
    protected virtual void SetDirection() // 캐릭터 좌우 반전 설정
    {
        // 오른쪽 이동 후 정지 시 오른쪽을 바라보게 왼쪽 이동 후 정지 시 왼쪽을 바라보게 설정
        if (characterRigidbody2D.velocity.x > fMinFlipSpeed)
        {
            //trPuppet.localScale = Vector2.one;
            trPuppet.localRotation = Global.normalRotation;
        }
        else if (characterRigidbody2D.velocity.x < -fMinFlipSpeed)
        {
            //trPuppet.localScale = Global.flippedScale;
            trPuppet.localRotation = Global.flippedRotation;
        }
    }

    public virtual void Stop() // 정지
    {
        characterRigidbody2D.velocity = Vector2.zero;
        Move(Vector2.zero);
    }
}
