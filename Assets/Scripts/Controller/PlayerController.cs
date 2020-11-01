using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player 클래스를 관리하는 클래스
public class PlayerController : Singleton<PlayerController>
{
    private Player player;

    private bool canMove;

    void Start()
    {
        player = Player.Instance;

        canMove = true;
    }

    void Update()
    {
        UpdateMovement();
    }

    // 입력을 받아 이동 명령을 내림
    private void UpdateMovement()
    {
        if (!canMove) return;

        Vector2 moveHorizontal = new Vector2();

        if(Input.GetKey(KeyCode.LeftArrow))
            moveHorizontal.x = -1.0f;
        else if (Input.GetKey(KeyCode.RightArrow))
            moveHorizontal.x = 1.0f;

        player.Move(moveHorizontal);
    }
}
