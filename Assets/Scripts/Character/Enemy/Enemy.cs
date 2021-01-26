using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일반적인 적 스펙을 명시하는 클래스
public class Enemy : Character
{
    [Header("Target")]
    [SerializeField] protected GameObject player = null;

    [Header("Attack")]
    [SerializeField] protected float fAtkRange = 1f;

    protected override void Initialization()
    {
        base.Initialization();

        FindPlayer();
    }

    // 플레이어 탐색
    protected virtual bool FindPlayer()
    {
        player = FindObjectOfType<Player>().gameObject;

        return (player != null);
    }

    // 플레이어 추격
    public void ChasePlayer()
    {
        if (!FindPlayer()) return;

        ChaseRoutine();
        CheckAttack();
    }

    // 추격 루틴
    protected virtual void ChaseRoutine()
    {

    }

    // 공격 조건 파악
    protected virtual void CheckAttack()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist.CompareTo(dist) < 0)
        {
            Attack();
        }
    }

    // 플레이어 공격
    public void Attack()
    {
        
    }
}
