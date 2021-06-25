using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 스펙을 명시하는 클래스
public class Character : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] protected Transform trPuppet = null;              // 캐릭터 본체
    [SerializeField] protected CharacterAudio characterAudio = null;   // 캐릭터 사운드
    [SerializeField] protected CharacterEffect characterEffect = null; //캐릭터 파티클 효과

    [Header("Component")]
    [SerializeField] protected Animator characterAnimator = null; //애니메이터
    [SerializeField] protected Rigidbody2D characterRigidbody2D = null;
    [SerializeField] protected Collider2D characterCollider2D = null;

    [Header("Status")]
    [SerializeField] protected GroundType groundType;
    [SerializeField] protected bool isAlive;

    [Header("Movement")]
    [SerializeField] protected float fMoveSpeed = 4f;
    [SerializeField] protected float fMinFlipSpeed = 0.1f;    

    protected int animatorMoveSpeed;
    protected int animatorIsAlive;
    protected bool canMove;
    
    public bool IsAlive { get { return isAlive; } protected set { isAlive = value; } }
    public virtual bool CanMove { get { return IsAlive && canMove; } protected set { canMove = value; } }

    public Transform TrPuppet { get {return trPuppet; } }

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        characterAnimator = trPuppet.GetComponent<Animator>();
        characterRigidbody2D = GetComponent<Rigidbody2D>();
        characterCollider2D = GetComponent<Collider2D>();

        animatorMoveSpeed = Animator.StringToHash("MoveSpeed");
        animatorIsAlive = Animator.StringToHash("IsAlive");

        IsAlive = true;
        CanMove = true;
        groundType = GroundType.Dirt;

        characterAnimator.SetFloat(animatorMoveSpeed, 0f);
        characterAnimator.SetBool(animatorIsAlive, true);
    }

    public virtual void Move(Vector2 movementInput) // 좌우 이동
    {
        if (!CanMove) return;

        // 이동속도 설정
        characterRigidbody2D.velocity = new Vector2(movementInput.x * fMoveSpeed, characterRigidbody2D.velocity.y);

        SetDirection();

        // 애니메이터 재생 속도 설정
        float fNormalizedMoveSpeed = Mathf.Abs(characterRigidbody2D.velocity.x) / fMoveSpeed;

        characterAnimator.SetFloat(animatorMoveSpeed, fNormalizedMoveSpeed);
        
        characterAudio?.PlaySteps(groundType, fNormalizedMoveSpeed);
        characterEffect?.PlayEffects(groundType, fNormalizedMoveSpeed);
    }
    protected virtual void SetDirection() // 캐릭터 좌우 반전
    {
        // 오른쪽 이동 후 정지 시 오른쪽을 바라보게 왼쪽 이동 후 정지 시 왼쪽을 바라보게 설정
        // 캐릭터 움직이는 효과(파티클) 때문에 Rotation으로 Flip 설정 [임시]
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

    public void Stop() // 정지
    {
        characterRigidbody2D.velocity = new Vector2(0f, characterRigidbody2D.velocity.y);
        Move(Vector2.zero);
    }

    public virtual void Die() // 사망
    {
        if (!IsAlive) return;

        characterAnimator.SetBool(animatorIsAlive, false);
        IsAlive = false;
    }
}
