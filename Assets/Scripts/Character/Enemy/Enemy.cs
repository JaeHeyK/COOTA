using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일반적인 적 스펙을 명시하는 클래스
public class Enemy : Character
{
    [Header("Target")]
    [SerializeField] protected Player target = null;

    [Header("Attack")]
    [SerializeField] protected float fAtkRange = 1f;

    protected int animatorIsDead;
    protected int animatorAttack;

    public bool HasTarget { get { return (target != null && target.IsAlive); } }

    protected override void Initialization()
    {
        base.Initialization();

        animatorIsDead = Animator.StringToHash("IsDead");
        animatorAttack = Animator.StringToHash("Attack");

        characterAnimator.SetBool(animatorIsDead, false);
        
        FindTarget();
    }

    // 플레이어 탐색
    protected virtual bool FindTarget()
    {
        target = FindObjectOfType<Player>();

        return HasTarget;
    }

    // 플레이어 추격
    public void ChasePlayer()
    {
        if (!FindTarget()) return;

        ChaseRoutine();
        if (CheckAttack())
        {
            Attack();
        }
    }

    // 추격 루틴
    protected virtual void ChaseRoutine()
    {
        if (!HasTarget) return;

        // X축 이동
        var dir = new Vector2(target.transform.position.x - transform.position.x, 0).normalized;

        Move(dir);
    }

    // 공격 조건 파악
    protected virtual bool CheckAttack()
    {
        if (!HasTarget) return false;

        // X축으로 거리 측정
        float dist = Mathf.Abs(transform.position.x - target.transform.position.x);

        return (dist.CompareTo(fAtkRange) < 0);
    }

    // 플레이어 공격
    public void Attack()
    {
        if (!HasTarget) return;

        characterAnimator.SetTrigger(animatorAttack);
    }

    // 적 제거
    public virtual void Die()
    {
        if (!IsAlive) return;

        characterAnimator.SetBool(animatorIsDead, true);
        IsAlive = false;

        Destroy(this.gameObject, 1f);
    }
}
