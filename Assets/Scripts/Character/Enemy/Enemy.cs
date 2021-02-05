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

    protected int animatorAttack;

    public bool HasTarget { get { return (target != null && target.IsAlive); } }

    protected override void Initialization()
    {
        base.Initialization();

        animatorAttack = Animator.StringToHash("Attack");

        FindTarget();
    }

    protected virtual bool FindTarget()   // 플레이어 탐색
    {
        target = FindObjectOfType<Player>();

        return HasTarget;
    }

    public void ChasePlayer() // 플레이어 추격
    {
        if (!FindTarget()) return;

        if (CheckAttack())
        {
            Attack();
        }
        else
        {
            ChaseRoutine();
        }
    }

    protected virtual void ChaseRoutine() // 추격 루틴
    {
        if (!HasTarget) return;

        // X축 이동
        var dir = new Vector2(target.transform.position.x - transform.position.x, 0).normalized;

        Move(dir);
    }

    protected virtual bool CheckAttack()  // 공격 조건 파악
    {
        if (!HasTarget) return false;

        // X축으로 거리 측정
        float dist = Mathf.Abs(transform.position.x - target.transform.position.x);

        return (dist.CompareTo(fAtkRange) < 0);
    }

    public void Attack() // 플레이어 공격
    {
        if (!HasTarget) return;

        characterAnimator.SetTrigger(animatorAttack);
    }
}
