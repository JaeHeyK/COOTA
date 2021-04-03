using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일반적인 적 스펙을 명시하는 클래스
public class Enemy : Character
{
    protected enum State { idle, chase, attack };

    [Header("Target")]
    [SerializeField] protected Player target = null;

    [Header("Attack")]
    [SerializeField] protected float fAtkRange = 0.1f;
    [SerializeField] protected float fChsRange = 1f;

    [Header("Status")]
    [SerializeField] protected bool isChasing;
    [SerializeField] protected bool isAttacking;

    protected int animatorIdle;
    protected int animatorAttack;
    protected int animatorChase;

    public bool HasTarget { get { return (target != null && target.IsAlive); } }
    public bool IsChasing { get { return isChasing;} protected set {this.isChasing = value;}}
    public bool IsAttacking {get { return isAttacking;} protected set {this.isAttacking = value;}}

    protected override void Initialization()
    {
        base.Initialization();

        animatorIdle = Animator.StringToHash("Idle");
        animatorAttack = Animator.StringToHash("Attack");
        animatorChase = Animator.StringToHash("Chase");
        
        FindTarget();
    }

    public void ChasePlayer() // 플레이어 추격
    {
        if ( !FindTarget() ) return;

        if (CheckChase())
        {
            IsChasing = true;
            IsAttacking = false;
            Debug.Log("Chasing");
            ChaseRoutine();
        }
        else if (CheckAttack())
        {   
            IsChasing = false;
            IsAttacking = true;
            Attack();
        }
    }

    protected virtual bool FindTarget()   // 플레이어 탐색
    {
        target = FindObjectOfType<Player>();

        return HasTarget;
    }    

    protected virtual bool CheckAttack()  // 공격 여부 판단
    {
        if (!HasTarget) return false;

        // X축으로 거리 측정
        float dist = Mathf.Abs(transform.position.x - target.transform.position.x);

        return (dist.CompareTo(fAtkRange) < 0);
    }

    protected void Attack() // 플레이어 공격
    {
        Stop();
        characterAnimator.SetTrigger(animatorAttack);
    }
    protected virtual bool CheckChase()  // 추격 여부 판단
    {
        if (!HasTarget) return false;

        // X축으로 거리 측정
        float dist = Mathf.Abs(transform.position.x - target.transform.position.x);

        return (dist.CompareTo(fChsRange) < 0);
    }

    protected virtual void ChaseRoutine() // 추격 루틴
    {
        if (!HasTarget) return;
        characterAnimator.SetTrigger(animatorChase);
        // X축 이동
        var dir = new Vector2(target.transform.position.x - transform.position.x, 0).normalized;
        Move(dir);
    }
}
