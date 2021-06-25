using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Enemy 이동을 관리하는 클래스
[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Character target;

    
    public float chaseDistance = 4f;
    public float attackDistance = 1f;
    //public float attackDistance = 1f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPoint = false;

    Seeker seeker;
    Rigidbody2D rb;



    void Start()
    {
        Initialization();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

   
    private void Initialization() //초기화
    {
        enemy = GetComponent<Enemy>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void UpdatePath() {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.TrPuppet.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0; 
        }
    }

    void  Update()
    {
        if(path == null)
        {
            return;
        }
        
        float playerDistance = Vector2.Distance(rb.position, target.TrPuppet.position);

        Debug.Log("Player distance: " + playerDistance);

        if (playerDistance < attackDistance)
        {
            enemy.Attack();
        }
        else if (playerDistance < chaseDistance)
        {
            UpdateMovement();
        } else {
            enemy.Idle();
        }

    }

    private void UpdateMovement() 
    {

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPoint = true;
            return ;
        } else
        {
            reachedEndOfPoint = false;
        }
        
       

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;


        enemy.Move(direction);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        


        // enemy.ChasePlayer();
        // Debug.Log("HasTarget: " + enemy.HasTarget);
        // Debug.Log("IsAttacking: " + enemy.IsAttacking);
        // Debug.Log("IsChasing: " + enemy.IsChasing);

        // Vector2 moveVector = Vector2.zero;


        // if(enemy.IsChasing) {
        //     moveVector
        // }
        // Vector2 moveVector = Vector2.zero;
        // enemy.Move(moveVector);
    }
}
