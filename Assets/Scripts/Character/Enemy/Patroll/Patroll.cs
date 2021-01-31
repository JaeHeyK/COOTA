using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 정찰병 스펙을 명시하는 클래스
public class Patroll : Enemy
{
    protected override void Initialization()
    {
        base.Initialization();
    }

    protected override bool FindTarget()   // 플레이어 탐색
    {
        target = FindObjectOfType<Player>();

        if (target.IsHiding) target = null;

        return HasTarget;
    }

    protected override void ChaseRoutine() // 추격 루틴
    {
    }

    protected override bool CheckAttack() // 공격 조건 파악
    {
        return false;
    }

    public override void Die() // 사망
    {
    }
}
