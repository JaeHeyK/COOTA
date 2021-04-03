using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy 이동을 관리하는 클래스
[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Character target;

    void Start()
    {
        Initialization();
    }
   
    private void Initialization() //초기화
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement() 
    {
        enemy.ChasePlayer();

        Vector2 moveVector = Vector2.zero;

        Debug.Log("HasTarget: " + enemy.HasTarget);
        Debug.Log("IsAttacking: " + enemy.IsAttacking);
        Debug.Log("IsChasing: " + enemy.IsChasing);

        // if(enemy.IsChasing) {
        //     moveVector
        // }
        // Vector2 moveVector = Vector2.zero;
        // enemy.Move(moveVector);
    }
}
